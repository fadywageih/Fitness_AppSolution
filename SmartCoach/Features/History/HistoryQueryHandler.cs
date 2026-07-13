using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCoach.Persistence;
using System.Security.Claims;

namespace SmartCoach.Features.History
{

    public class HistoryQueryHandler : IRequestHandler<HistoryQuery, object>
    {
        private readonly SmartCoachDbContext _context;
        private readonly IHttpContextAccessor _http;

        public HistoryQueryHandler(SmartCoachDbContext context, IHttpContextAccessor http) { _context = context; _http = http; }

        public async Task<object> Handle(HistoryQuery query, CancellationToken ct)
        {
            var userIdClaim = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.TryParse(userIdClaim, out var uid) ? uid : Guid.Empty;

            if (query.SessionId.HasValue)
            {
                var messages = await _context.Messages
                    .Where(m => m.SessionId == query.SessionId.Value)
                    .OrderBy(m => m.SentAt)
                    .Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)
                    .Select(m => new { m.Id, m.Sender, m.Content, m.SentAt })
                    .ToListAsync(ct);
                return messages;
            }

            var sessions = await _context.Sessions
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedAt)
                .Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)
                .Select(s => new { s.Id, s.CreatedAt, MessageCount = s.Messages.Count })
                .ToListAsync(ct);
            return sessions;
        }
    }
}
