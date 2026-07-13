using MediatR;

namespace Progress_Tracking.Features.ViewProgress
{

    public static class ViewProgressEndpoint
    {
        public static void MapViewProgressEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/progress", async (string? period, DateTime? startDate, DateTime? endDate, ISender sender) =>
            {
                var result = await sender.Send(new ViewProgressQuery(period, startDate, endDate));
                return result is null ? Results.Unauthorized()
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("ViewProgress").WithTags("Progress").RequireAuthorization();
        }
    }
}
