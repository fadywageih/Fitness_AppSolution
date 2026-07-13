using MediatR;
using NutritionService.Persistence;

namespace NutritionService.Features.MealDetail
{

    public class MealDetailQueryHandler : IRequestHandler<MealDetailQuery, MealDetailResponse?>
    {
        private readonly NutritionDbContext _context;
        public MealDetailQueryHandler(NutritionDbContext context) => _context = context;

        public async Task<MealDetailResponse?> Handle(MealDetailQuery query, CancellationToken ct)
        {
            var meal = await _context.Meals.FindAsync(new object[] { query.Id }, ct);
            return meal is null ? null : new MealDetailResponse(meal.Id, meal.Name, meal.Type, meal.Calories, meal.Protein, meal.Carbs, meal.Fat, meal.Ingredients, meal.Instructions, meal.Allergens, meal.Tags);
        }
    }
}
