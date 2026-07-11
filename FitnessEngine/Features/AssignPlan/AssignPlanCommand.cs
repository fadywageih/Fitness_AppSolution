using MediatR;

namespace FitnessEngine.Features.AssignPlan
{

    public record AssignPlanCommand : IRequest<AssignPlanResponse?>;

    public record AssignPlanResponse(Guid PlanId, string PlanName, int DurationWeeks, int WorkoutsPerWeek);
}
