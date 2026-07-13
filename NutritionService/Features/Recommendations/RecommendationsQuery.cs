using MediatR;

namespace NutritionService.Features.Recommendations
{

    public record RecommendationsQuery(string? MealType, int? MaxCalories, int? MinProtein, int Page = 1, int PageSize = 20) : IRequest<RecommendationsResponse>;

    public record RecommendationsResponse(int UserDailyGoalCalories, List<MealResponse> RecommendedMeals);

    public record MealResponse(Guid Id, string Name, string Type, int Calories, int Protein, int Carbs, int Fat);
}
