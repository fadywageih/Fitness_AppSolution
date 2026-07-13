using MediatR;

namespace NotificationService.Features.MarkRead
{

    public static class MarkReadEndpoint
    {
        public static void MapMarkReadEndpoint(this WebApplication app)
        {
            app.MapPut("/api/v1/notifications/{id}/read", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new MarkReadCommand(id));
                return result
                    ? Results.Ok(new { isSuccess = true, message = "Marked as read", statusCode = 200 })
                    : Results.NotFound(new { message = "Notification not found" });
            }).WithName("MarkRead").WithTags("Notifications").RequireAuthorization();
        }
    }
}
