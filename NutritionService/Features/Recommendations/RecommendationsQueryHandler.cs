using MediatR;
using Microsoft.EntityFrameworkCore;
using NutritionService.Persistence;

namespace NutritionService.Features.Recommendations
{

    public class RecommendationsQueryHandler : IRequestHandler<RecommendationsQuery, RecommendationsResponse>
    {
        private readonly NutritionDbContext _context;

        public RecommendationsQueryHandler(NutritionDbContext context) => _context = context;

        public async Task<RecommendationsResponse> Handle(RecommendationsQuery query, CancellationToken ct)
        {
            var meals = _context.Meals.AsQueryable();

            if (!string.IsNullOrEmpty(query.MealType))
                meals = meals.Where(m => m.Type == query.MealType);
            if (query.MaxCalories.HasValue)
                meals = meals.Where(m => m.Calories <= query.MaxCalories.Value);
            if (query.MinProtein.HasValue)
                meals = meals.Where(m => m.Protein >= query.MinProtein.Value);

            var result = await meals
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(m => new MealResponse(m.Id, m.Name, m.Type, m.Calories, m.Protein, m.Carbs, m.Fat))
                .ToListAsync(ct);

            return new RecommendationsResponse(2000, result);
        }
    }
}
