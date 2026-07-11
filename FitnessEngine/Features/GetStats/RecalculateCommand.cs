using MediatR;

namespace FitnessEngine.Features.GetStats
{

    public record RecalculateCommand(decimal? NewWeight) : IRequest<RecalculateResponse?>;

    public record RecalculateResponse(decimal Bmr, decimal Tdee, decimal CalorieTarget, string Status, bool PlanReassigned);
}
