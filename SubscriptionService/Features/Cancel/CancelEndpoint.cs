using MediatR;

namespace SubscriptionService.Features.Cancel
{

    public static class CancelEndpoint
    {
        public static void MapCancelEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/subscription/cancel", async (Guid userId, ISender sender) =>
            {
                var result = await sender.Send(new CancelCommand(userId));
                return result is null ? Results.NotFound(new { message = "No active subscription found" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("Cancel").WithTags("Subscription").RequireAuthorization();
        }
    }
}
