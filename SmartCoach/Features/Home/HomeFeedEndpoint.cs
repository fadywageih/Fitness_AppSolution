using MediatR;

namespace SmartCoach.Features.Home
{

    public static class HomeFeedEndpoint
    {
        public static void MapHomeFeedEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/home", async (ISender sender) =>
            {
                var result = await sender.Send(new HomeFeedQuery());
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("HomeFeed").WithTags("SmartCoach").RequireAuthorization();
        }
    }
}
