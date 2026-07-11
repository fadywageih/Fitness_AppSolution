using MediatR;

namespace FitnessEngine.Features.GetStats
{

    public record GetPlanConfigsQuery(int Goal, string Status, int Page = 1, int PageSize = 20) : IRequest<List<PlanConfigResponse>>;

    public record PlanConfigResponse(Guid Id, string PlanName, string Goal, string Status, int DurationWeeks, int WorkoutsPerWeek, string Description);
}
