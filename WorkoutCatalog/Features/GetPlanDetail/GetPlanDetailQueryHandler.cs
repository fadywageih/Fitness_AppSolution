using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutCatalog.Persistence;

namespace WorkoutCatalog.Features.GetPlanDetail
{

    public class GetWorkoutPlanDetailQueryHandler : IRequestHandler<GetWorkoutPlanDetailQuery, PlanDetailResponse?>
    {
        private readonly WorkoutCatalogDbContext _context;
        public GetWorkoutPlanDetailQueryHandler(WorkoutCatalogDbContext context) => _context = context;

        public async Task<PlanDetailResponse?> Handle(GetWorkoutPlanDetailQuery query, CancellationToken ct)
        {
            var plan = await _context.Plans
                .Include(p => p.Workouts)
                .FirstOrDefaultAsync(p => p.Id == query.Id, ct);

            if (plan is null) return null;

            return new PlanDetailResponse(
                plan.Id, plan.Name, plan.Description, plan.Difficulty.ToString(), plan.DurationWeeks,
                plan.Workouts.OrderBy(w => w.OrderIndex)
                    .Select(w => new PlanWorkoutItem(w.Id, w.Name, w.Category.ToString(), w.DurationMinutes))
                    .ToList()
            );
        }
    }
}
