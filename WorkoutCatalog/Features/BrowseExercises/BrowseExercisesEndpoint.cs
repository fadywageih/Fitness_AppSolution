using MediatR;

namespace WorkoutCatalog.Features.BrowseExercises
{

    public static class BrowseExercisesEndpoint
    {
        public static void MapBrowseExercisesEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/exercises", async (int? page, int? pageSize, ISender sender) =>
            {
                var result = await sender.Send(new BrowseExercisesQuery(page ?? 1, pageSize ?? 50));
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("BrowseExercises").WithTags("Exercises").RequireAuthorization();
        }
    }

}
