using MediatR;

namespace SubscriptionService.Features.Upgrade
{

    public record UpgradeCommand(Guid UserId, string PlanTier, int DurationMonths) : IRequest<UpgradeResponse?>;

    public record UpgradeResponse(string Tier, DateTime ExpiresAt, string Message);

}
