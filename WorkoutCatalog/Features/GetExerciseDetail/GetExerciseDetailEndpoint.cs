using MediatR;

namespace WorkoutCatalog.Features.GetExerciseDetail
{

    public static class GetExerciseDetailEndpoint
    {
        public static void MapGetExerciseDetailEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/exercises/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetExerciseDetailQuery(id));
                return result is null ? Results.NotFound(new { message = "Exercise not found" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("GetExerciseDetail").WithTags("Exercises").RequireAuthorization();
        }
    }
}
