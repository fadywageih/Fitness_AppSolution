using FitnessEngine.Persistence;
using MediatR;

namespace FitnessEngine.Features.GetStats
{

    public class GetPlanDetailQueryHandler : IRequestHandler<GetPlanDetailQuery, PlanConfigResponse?>
    {
        private readonly FitnessDbContext _context;

        public GetPlanDetailQueryHandler(FitnessDbContext context) => _context = context;

        public async Task<PlanConfigResponse?> Handle(GetPlanDetailQuery query, CancellationToken cancellationToken)
        {
            var plan = await _context.PlanConfigs.FindAsync(new object[] { query.PlanId }, cancellationToken);

            return plan is null ? null : new PlanConfigResponse(plan.Id, plan.PlanName, plan.Goal.ToString(), plan.Status.ToString(), plan.DurationWeeks, plan.WorkoutsPerWeek, plan.Description);
        }
    }
}
