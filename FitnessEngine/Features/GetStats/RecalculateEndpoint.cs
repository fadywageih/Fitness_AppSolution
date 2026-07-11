using MediatR;

namespace FitnessEngine.Features.GetStats
{
    public static class RecalculateEndpoint
    {
        public static void MapRecalculateEndpoint(this WebApplication app)
        {
            app.MapPut("/api/v1/fitness/recalculate", async (decimal? newWeight, ISender sender) =>
            {
                var command = new RecalculateCommand(newWeight);
                var result = await sender.Send(command);
                return result is null ? Results.BadRequest(new { isSuccess = false, message = "Calculate metrics first" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            })
            .WithName("Recalculate").WithTags("Fitness").RequireAuthorization();
        }
    }
}
