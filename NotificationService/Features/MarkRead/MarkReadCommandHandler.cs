
using MediatR;
using Microsoft.EntityFrameworkCore;
using NotificationService.Persistence;
using System.Security.Claims;

namespace NotificationService.Features.MarkRead
{

    public class MarkReadCommandHandler : IRequestHandler<MarkReadCommand, bool>
    {
        private readonly NotificationDbContext _context;
        private readonly IHttpContextAccessor _http;

        public MarkReadCommandHandler(NotificationDbContext context, IHttpContextAccessor http) { _context = context; _http = http; }

        public async Task<bool> Handle(MarkReadCommand command, CancellationToken ct)
        {
            var userIdClaim = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.TryParse(userIdClaim, out var uid) ? uid : Guid.Empty;

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == command.Id && n.UserId == userId, ct);

            if (notification is null) return false;

            notification.MarkAsRead();
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }

}
