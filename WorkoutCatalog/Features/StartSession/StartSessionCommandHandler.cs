using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkoutCatalog.Domain.Entities;
using WorkoutCatalog.Persistence;

namespace WorkoutCatalog.Features.StartSession
{

    public class StartSessionCommandHandler : IRequestHandler<StartSessionCommand, StartSessionResponse?>
    {
        private readonly WorkoutCatalogDbContext _context;
        private readonly IHttpContextAccessor _http;

        public StartSessionCommandHandler(WorkoutCatalogDbContext context, IHttpContextAccessor http) { _context = context; _http = http; }

        public async Task<StartSessionResponse?> Handle(StartSessionCommand command, CancellationToken ct)
        {
            var userIdClaim = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId)) return null;

            var exists = await _context.Sessions.AnyAsync(s => s.UserId == userId && s.WorkoutId == command.WorkoutId && s.Status == "Active", ct);
            if (exists)
            {
                var existing = await _context.Sessions.FirstAsync(s => s.UserId == userId && s.WorkoutId == command.WorkoutId && s.Status == "Active", ct);
                return new StartSessionResponse(existing.Id, existing.Status, existing.StartedAt);
            }

            var session = WorkoutSession.Create(userId, command.WorkoutId);
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync(ct);

            return new StartSessionResponse(session.Id, session.Status, session.StartedAt);
        }
    }

}
