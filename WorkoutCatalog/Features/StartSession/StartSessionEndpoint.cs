using MediatR;

namespace WorkoutCatalog.Features.StartSession
{

    public static class StartSessionEndpoint
    {
        public static void MapStartSessionEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/workouts/{id}/start", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new StartSessionCommand(id));
                return result is null ? Results.NotFound(new { message = "Workout not found" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 201 });
            }).WithName("StartSession").WithTags("Workouts").RequireAuthorization();
        }
    }
}
