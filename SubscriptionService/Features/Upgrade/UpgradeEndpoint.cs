using MediatR;

namespace SubscriptionService.Features.Upgrade
{

    public static class UpgradeEndpoint
    {
        public static void MapUpgradeEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/subscription/upgrade", async (
                Guid userId, string planTier, int durationMonths, ISender sender) =>
            {
                var result = await sender.Send(new UpgradeCommand(userId, planTier, durationMonths));
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("Upgrade").WithTags("Subscription").RequireAuthorization();
        }
    }
}
