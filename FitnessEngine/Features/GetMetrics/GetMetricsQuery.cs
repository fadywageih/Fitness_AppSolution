using MediatR;

namespace FitnessEngine.Features.GetMetrics
{
    public record GetMetricsQuery : IRequest<GetMetricsResponse?>;

    public record GetMetricsResponse(decimal Bmr, decimal Tdee, decimal CalorieTarget, string Status, DateTime CalculatedAt);

}
