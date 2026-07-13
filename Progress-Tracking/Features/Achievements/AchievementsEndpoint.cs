using MediatR;

namespace Progress_Tracking.Features.Achievements
{

    public static class AchievementsEndpoint
    {
        public static void MapAchievementsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/progress/achievements", async (ISender sender) =>
            {
                var result = await sender.Send(new AchievementsQuery());
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("Achievements").WithTags("Progress").RequireAuthorization();
        }
    }
}
