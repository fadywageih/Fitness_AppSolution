using MediatR;

namespace FitnessEngine.Features.SubmitStats
{

    public static class SubmitStatsEndpoint
    {
        public static void MapSubmitStatsEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/fitness/weight-goal-activity", async (
                decimal weight, decimal height, int age, string gender, int goal, int activityLevel, ISender sender) =>
            {
                var validator = new SubmitStatsCommandValidator();
                var command = new SubmitStatsCommand(weight, height, age, gender, goal, activityLevel);
                var vr = await validator.ValidateAsync(command);
                if (!vr.IsValid)
                    return Results.BadRequest(new { isSuccess = false, errors = vr.Errors.Select(e => e.ErrorMessage), statusCode = 400 });

                var result = await sender.Send(command);
                return result is null ? Results.Unauthorized() : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            })
            .WithName("SubmitStats").WithTags("Fitness").RequireAuthorization();
        }
    }
}
