using MediatR;

namespace FitnessEngine.Features.GetMetrics
{
    public static class GetMetricsEndpoint
    {
        public static void MapGetMetricsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/fitness/metrics", async (ISender sender) =>
            {
                var result = await sender.Send(new GetMetricsQuery());
                return result is null ? Results.NotFound(new { message = "Metrics not found" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            })
            .WithName("GetMetrics").WithTags("Fitness").RequireAuthorization();
        }
    }
}
