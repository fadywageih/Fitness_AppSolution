using MediatR;

namespace SubscriptionService.Features.Status
{

    public static class SubscriptionStatusEndpoint
    {
        public static void MapSubscriptionStatusEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/subscription/status/{userId}", async (Guid userId, ISender sender) =>
            {
                var result = await sender.Send(new SubscriptionStatusQuery(userId));
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("SubscriptionStatus").WithTags("Subscription").RequireAuthorization();
        }
    }

}
