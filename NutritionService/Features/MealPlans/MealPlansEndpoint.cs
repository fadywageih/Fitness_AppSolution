using MediatR;

namespace NutritionService.Features.MealPlans
{

    public static class MealPlansEndpoint
    {
        public static void MapMealPlansEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/nutrition/meal-plans", async (int? calories, int? page, int? pageSize, ISender sender) =>
            {
                var result = await sender.Send(new MealPlansQuery(calories, page ?? 1, pageSize ?? 20));
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("MealPlans").WithTags("Nutrition").RequireAuthorization();
        }
    }
}
