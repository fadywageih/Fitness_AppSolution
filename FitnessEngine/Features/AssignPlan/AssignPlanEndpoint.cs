using MediatR;

namespace FitnessEngine.Features.AssignPlan
{

    public static class AssignPlanEndpoint
    {
        public static void MapAssignPlanEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/fitness/assign-plan", async (ISender sender) =>
            {
                var result = await sender.Send(new AssignPlanCommand());
                return result is null ? Results.BadRequest(new { isSuccess = false, message = "Calculate metrics first" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            })
            .WithName("AssignPlan").WithTags("Fitness").RequireAuthorization();
        }
    }
}
