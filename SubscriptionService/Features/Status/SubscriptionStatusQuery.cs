using MediatR;

namespace SubscriptionService.Features.Status
{

    public record SubscriptionStatusQuery(Guid UserId) : IRequest<SubscriptionStatusResponse>;

    public record SubscriptionStatusResponse(string Tier, string Status, DateTime? ExpiresAt);
}
