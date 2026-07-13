using MediatR;

namespace SubscriptionService.Features.Cancel
{

    public record CancelCommand(Guid UserId) : IRequest<CancelResponse?>;

    public record CancelResponse(bool Cancelled, string Message);

}
