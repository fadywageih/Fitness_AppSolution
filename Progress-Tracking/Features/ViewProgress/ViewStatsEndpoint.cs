using MediatR;

namespace Progress_Tracking.Features.ViewProgress
{

    public static class ViewStatsEndpoint
    {
        public static void MapViewStatsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/progress/stats/{userId}", async (Guid userId, ISender sender) =>
            {
                var result = await sender.Send(new ViewStatsQuery(userId));
                return result is null ? Results.NotFound(new { message = "Stats not found" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("ViewStats").WithTags("Progress").RequireAuthorization();
        }
    }

}
