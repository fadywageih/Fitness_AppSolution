using MediatR;
using Microsoft.EntityFrameworkCore;
using NotificationService.Persistence;
using System.Security.Claims;

namespace NotificationService.Features.GetNotifications
{

    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, List<NotificationResponse>>
    {
        private readonly NotificationDbContext _context;
        private readonly IHttpContextAccessor _http;

        public GetNotificationsQueryHandler(NotificationDbContext context, IHttpContextAccessor http) { _context = context; _http = http; }

        public async Task<List<NotificationResponse>> Handle(GetNotificationsQuery query, CancellationToken ct)
        {
            var userIdClaim = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.TryParse(userIdClaim, out var uid) ? uid : Guid.Empty;

            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.IsRead)
                .ThenByDescending(n => n.CreatedAt)
                .Select(n => new NotificationResponse(n.Id, n.Title, n.Message, n.IconUrl, n.IsRead, n.CreatedAt))
                .ToListAsync(ct);
        }
    }
}
