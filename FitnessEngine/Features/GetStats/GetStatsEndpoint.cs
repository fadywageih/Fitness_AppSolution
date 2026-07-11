using MediatR;

namespace FitnessEngine.Features.GetStats
{

    public static class GetStatsEndpoint
    {
        public static void MapGetStatsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/fitness/stats", async (ISender sender) =>
            {
                var result = await sender.Send(new GetStatsQuery());
                return result is null ? Results.NotFound(new { message = "Stats not found" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            })
            .WithName("GetStats").WithTags("Fitness").RequireAuthorization();
        }
    }
}
