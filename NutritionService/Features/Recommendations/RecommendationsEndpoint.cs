using MediatR;

namespace NutritionService.Features.Recommendations
{

    public static class RecommendationsEndpoint
    {
        public static void MapRecommendationsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/nutrition/recommendations", async (
                string? mealType, int? maxCalories, int? minProtein, int? page, int? pageSize, ISender sender) =>
            {
                var result = await sender.Send(new RecommendationsQuery(mealType, maxCalories, minProtein, page ?? 1, pageSize ?? 20));
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("Recommendations").WithTags("Nutrition").RequireAuthorization();
        }
    }
}
