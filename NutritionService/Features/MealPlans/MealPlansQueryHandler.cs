using MediatR;
using Microsoft.EntityFrameworkCore;
using NutritionService.Persistence;

namespace NutritionService.Features.MealPlans
{

    public class MealPlansQueryHandler : IRequestHandler<MealPlansQuery, List<MealPlanResponse>>
    {
        private readonly NutritionDbContext _context;
        public MealPlansQueryHandler(NutritionDbContext context) => _context = context;

        public async Task<List<MealPlanResponse>> Handle(MealPlansQuery query, CancellationToken ct)
        {
            var plans = _context.MealPlans.Include(p => p.Items).AsQueryable();

            if (query.Calories.HasValue)
                plans = plans.Where(p => query.Calories.Value >= p.TargetCalorieMin && query.Calories.Value <= p.TargetCalorieMax);

            return await plans.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)
                .Select(p => new MealPlanResponse(p.Id, p.Name, p.TargetCalorieMin, p.TargetCalorieMax, p.Items.Count))
                .ToListAsync(ct);
        }
    }
}
