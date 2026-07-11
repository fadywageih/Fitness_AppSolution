using FitnessEngine.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessEngine.Features.GetStats
{

    public class GetPlanConfigsQueryHandler : IRequestHandler<GetPlanConfigsQuery, List<PlanConfigResponse>>
    {
        private readonly FitnessDbContext _context;

        public GetPlanConfigsQueryHandler(FitnessDbContext context) => _context = context;

        public async Task<List<PlanConfigResponse>> Handle(GetPlanConfigsQuery query, CancellationToken cancellationToken)
        {
            var configs = await _context.PlanConfigs
                .Where(p => (query.Goal == 0 || (int)p.Goal == query.Goal) &&
                            (string.IsNullOrEmpty(query.Status) || p.Status.ToString() == query.Status))
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(p => new PlanConfigResponse(p.Id, p.PlanName, p.Goal.ToString(), p.Status.ToString(), p.DurationWeeks, p.WorkoutsPerWeek, p.Description))
                .ToListAsync(cancellationToken);

            return configs;
        }
    }
}
