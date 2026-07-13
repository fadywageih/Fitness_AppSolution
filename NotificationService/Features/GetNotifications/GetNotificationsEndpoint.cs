using MediatR;

namespace NotificationService.Features.GetNotifications
{

    public static class GetNotificationsEndpoint
    {
        public static void MapGetNotificationsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/notifications", async (ISender sender) =>
            {
                var result = await sender.Send(new GetNotificationsQuery());
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("GetNotifications").WithTags("Notifications").RequireAuthorization();
        }
    }
}
