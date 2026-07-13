using MediatR;
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Persistence;

namespace SubscriptionService.Features.Status
{

    public class SubscriptionStatusQueryHandler : IRequestHandler<SubscriptionStatusQuery, SubscriptionStatusResponse>
    {
        private readonly SubscriptionDbContext _context;
        public SubscriptionStatusQueryHandler(SubscriptionDbContext context) => _context = context;

        public async Task<SubscriptionStatusResponse> Handle(SubscriptionStatusQuery query, CancellationToken ct)
        {
            var sub = await _context.Subscriptions.FirstOrDefaultAsync(s => s.UserId == query.UserId, ct);
            return sub is null
                ? new SubscriptionStatusResponse("Free", "Active", null)
                : new SubscriptionStatusResponse(sub.Tier, sub.Status, sub.ExpiresAt);
        }
    }
}
