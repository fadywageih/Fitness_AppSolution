using MediatR;

namespace NutritionService.Features.Recommendations
{

    public record RecommendationsByUserQuery(Guid UserId, string? MealType, int? MaxCalories, int? MinProtein, int Page = 1, int PageSize = 20) : IRequest<RecommendationsResponse>;

}
