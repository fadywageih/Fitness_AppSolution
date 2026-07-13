using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedContracts.Events;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Persistence;

namespace SubscriptionService.Features.Upgrade
{

    public class UpgradeCommandHandler : IRequestHandler<UpgradeCommand, UpgradeResponse?>
    {
        private readonly SubscriptionDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpgradeCommandHandler(SubscriptionDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<UpgradeResponse?> Handle(UpgradeCommand command, CancellationToken ct)
        {
            var amount = command.DurationMonths * 9.99m;

            var billingLog = BillingLog.Create(command.UserId, command.PlanTier, command.DurationMonths, amount, "Success");
            _context.BillingLogs.Add(billingLog);

            var sub = await _context.Subscriptions.FirstOrDefaultAsync(s => s.UserId == command.UserId, ct);
            var expiresAt = DateTime.UtcNow.AddMonths(command.DurationMonths);

            if (sub is null)
            {
                sub = UserSubscription.Create(command.UserId, command.PlanTier, expiresAt);
                _context.Subscriptions.Add(sub);
            }
            else
            {
                sub.Upgrade(command.PlanTier, expiresAt);
            }

            await _context.SaveChangesAsync(ct);

            await _publishEndpoint.Publish(new SubscriptionUpgradedEvent(command.UserId, command.PlanTier, expiresAt), ct);

            return new UpgradeResponse(sub.Tier, sub.ExpiresAt!.Value, "Upgrade successful");
        }
    }
}
