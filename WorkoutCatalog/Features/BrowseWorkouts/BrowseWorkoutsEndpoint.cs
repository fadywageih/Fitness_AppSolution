using MediatR;

namespace WorkoutCatalog.Features.BrowseWorkouts
{

    public static class BrowseWorkoutsEndpoint
    {
        public static void MapBrowseWorkoutsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/workouts", async (
                int? category, int? difficulty, int? duration, string? search, int? page, int? pageSize, ISender sender) =>
            {
                var result = await sender.Send(new BrowseWorkoutsQuery(category, difficulty, duration, search, page ?? 1, pageSize ?? 20));
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            })
            .WithName("BrowseWorkouts").WithTags("Workouts").RequireAuthorization();
        }
    }

}
