using MediatR;

namespace NutritionService.Features.MealDetail
{

    public record MealDetailQuery(Guid Id) : IRequest<MealDetailResponse?>;

    public record MealDetailResponse(Guid Id, string Name, string Type, int Calories, int Protein, int Carbs, int Fat, string Ingredients, string Instructions, string? Allergens, string? Tags);
}
