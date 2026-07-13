using MediatR;

namespace Progress_Tracking.Features.ViewProgress
{

    public static class ViewUserProgressEndpoint
    {
        public static void MapViewUserProgressEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/progress/{userId}", async (Guid userId, ISender sender) =>
            {
                var result = await sender.Send(new ViewUserProgressQuery(userId));
                return result is null ? Results.NotFound(new { message = "User not found" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("ViewUserProgress").WithTags("Progress").RequireAuthorization();
        }
    }
}
