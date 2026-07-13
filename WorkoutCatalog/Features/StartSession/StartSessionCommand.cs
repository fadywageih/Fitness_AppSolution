using MediatR;

namespace WorkoutCatalog.Features.StartSession
{

    public record StartSessionCommand(Guid WorkoutId) : IRequest<StartSessionResponse?>;

    public record StartSessionResponse(Guid SessionId, string Status, DateTime StartedAt);
}
