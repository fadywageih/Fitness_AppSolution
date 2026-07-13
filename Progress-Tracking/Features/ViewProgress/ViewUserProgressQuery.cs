using MediatR;

namespace Progress_Tracking.Features.ViewProgress
{

    public record ViewUserProgressQuery(Guid UserId) : IRequest<ViewProgressResponse?>;
}
