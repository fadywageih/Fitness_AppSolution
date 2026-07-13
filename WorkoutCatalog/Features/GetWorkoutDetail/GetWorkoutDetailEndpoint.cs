using MediatR;

namespace WorkoutCatalog.Features.GetWorkoutDetail
{


    public static class GetWorkoutDetailEndpoint
    {
        public static void MapGetWorkoutDetailEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/workouts/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetWorkoutDetailQuery(id));
                return result is null ? Results.NotFound(new { message = "Workout not found" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            })
            .WithName("GetWorkoutDetail").WithTags("Workouts").RequireAuthorization();
        }
    }
}
