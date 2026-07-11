using MediatR;

namespace FitnessEngine.Features.Calculate
{

    public static class CalculateEndpoint
    {
        public static void MapCalculateEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/fitness/calculate", async (ISender sender) =>
            {
                var result = await sender.Send(new CalculateCommand());
                return result is null ? Results.NotFound(new { isSuccess = false, message = "Stats not found" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            })
            .WithName("Calculate").WithTags("Fitness").RequireAuthorization();
        }
    }
}
