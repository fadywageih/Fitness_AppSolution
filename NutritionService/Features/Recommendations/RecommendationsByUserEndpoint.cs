using MediatR;

namespace NutritionService.Features.Recommendations
{

    public static class RecommendationsByUserEndpoint
    {
        public static void MapRecommendationsByUserEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/nutrition/recommendations/{userId}", async (
                Guid userId, string? mealType, int? maxCalories, int? minProtein, int? page, int? pageSize, ISender sender) =>
            {
                var result = await sender.Send(new RecommendationsByUserQuery(userId, mealType, maxCalories, minProtein, page ?? 1, pageSize ?? 20));
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("RecommendationsByUser").WithTags("Nutrition").RequireAuthorization();
        }
    }
}
