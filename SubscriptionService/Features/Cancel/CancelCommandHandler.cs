using MediatR;
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Persistence;

namespace SubscriptionService.Features.Cancel
{

    public class CancelCommandHandler : IRequestHandler<CancelCommand, CancelResponse?>
    {
        private readonly SubscriptionDbContext _context;

        public CancelCommandHandler(SubscriptionDbContext context) => _context = context;

        public async Task<CancelResponse?> Handle(CancelCommand command, CancellationToken ct)
        {
            var sub = await _context.Subscriptions.FirstOrDefaultAsync(s => s.UserId == command.UserId && s.Tier != "Free", ct);
            if (sub is null) return null;

            sub.Cancel();
            await _context.SaveChangesAsync(ct);

            return new CancelResponse(true, "Subscription will be cancelled at the end of the billing period");
        }
    }
}
