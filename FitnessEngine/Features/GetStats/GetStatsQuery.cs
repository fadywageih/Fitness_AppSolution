using MediatR;

namespace FitnessEngine.Features.GetStats
{
    public record GetStatsQuery : IRequest<GetStatsResponse?>;

    public record GetStatsResponse(decimal Weight, decimal Height, int Age, string Gender, string Goal, string ActivityLevel);

}
