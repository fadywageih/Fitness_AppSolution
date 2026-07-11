using MediatR;

namespace FitnessEngine.Features.GetStats
{
    public record GetPlanDetailQuery(Guid PlanId) : IRequest<PlanConfigResponse?>;

}
