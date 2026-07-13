using MediatR;

namespace NutritionService.Features.MealDetail
{

    public static class MealDetailEndpoint
    {
        public static void MapMealDetailEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/nutrition/meals/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new MealDetailQuery(id));
                return result is null ? Results.NotFound(new { message = "Meal not found" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("MealDetail").WithTags("Nutrition").RequireAuthorization();
        }
    }
}
