using MediatR;

namespace SmartCoach.Features.History
{

    public static class HistoryEndpoint
    {
        public static void MapHistoryEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/smart-coach/history", async (Guid? sessionId, int? page, int? pageSize, ISender sender) =>
            {
                var result = await sender.Send(new HistoryQuery(sessionId, page ?? 1, pageSize ?? 20));
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("History").WithTags("SmartCoach").RequireAuthorization();
        }
    }
}
