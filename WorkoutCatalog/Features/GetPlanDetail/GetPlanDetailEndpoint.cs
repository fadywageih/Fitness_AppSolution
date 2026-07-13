using MediatR;

namespace WorkoutCatalog.Features.GetPlanDetail
{

    public static class GetPlanDetailEndpoint
    {
        public static void MapGetPlanDetailEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/workout-plans/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetWorkoutPlanDetailQuery(id));
                return result is null ? Results.NotFound(new { message = "Plan not found" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("GetPlanDetail").WithTags("Plans").RequireAuthorization();
        }
    }

}
