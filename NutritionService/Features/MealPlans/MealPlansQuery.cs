using MediatR;

namespace NutritionService.Features.MealPlans
{

    public record MealPlansQuery(int? Calories, int Page = 1, int PageSize = 20) : IRequest<List<MealPlanResponse>>;

    public record MealPlanResponse(Guid Id, string Name, int TargetCalorieMin, int TargetCalorieMax, int ItemCount);
}
