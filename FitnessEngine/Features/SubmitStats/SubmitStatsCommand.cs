using MediatR;

namespace FitnessEngine.Features.SubmitStats
{

    public record SubmitStatsCommand(
        decimal Weight,
        decimal Height,
        int Age,
        string Gender,
        int Goal,
        int ActivityLevel
    ) : IRequest<SubmitStatsResponse?>;

    public record SubmitStatsResponse(Guid UserId, string Message);
}
