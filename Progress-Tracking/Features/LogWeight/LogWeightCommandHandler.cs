using MediatR;
using Microsoft.EntityFrameworkCore;
using Progress_Tracking.Domain.Entities;
using Progress_Tracking.Persistence;
using System.Security.Claims;

namespace Progress_Tracking.Features.LogWeight
{

    public class LogWeightCommandHandler : IRequestHandler<LogWeightCommand, LogWeightResponse?>
    {
        private readonly ProgressDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogWeightCommandHandler(ProgressDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LogWeightResponse?> Handle(LogWeightCommand command, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId)) return null;

            var entry = WeightHistory.Create(userId, command.Weight, command.Date, command.Notes);
            _context.WeightHistory.Add(entry);

            var previous = await _context.WeightHistory
                .Where(w => w.UserId == userId && w.Date < command.Date)
                .OrderByDescending(w => w.Date)
                .FirstOrDefaultAsync(cancellationToken);

            var stats = await _context.UserStatistics.FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
            if (stats is null)
            {
                stats = UserStatistics.Create(userId);
                _context.UserStatistics.Add(stats);
            }

            await _context.SaveChangesAsync(cancellationToken);

            decimal? diff = previous is not null ? command.Weight - previous.Weight : null;
            decimal bmi = command.Weight / 1.75m / 1.75m; // Simplified - height should come from FitnessEngine

            return new LogWeightResponse(entry.Id, entry.Weight, Math.Round(bmi, 1), diff.HasValue ? Math.Round(diff.Value, 1) : null);
        }
    }
}
