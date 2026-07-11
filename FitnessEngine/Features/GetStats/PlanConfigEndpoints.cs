using MediatR;

namespace FitnessEngine.Features.GetStats
{

    public static class PlanConfigEndpoints
    {
        public static void MapPlanConfigEndpoints(this WebApplication app)
        {
            app.MapGet("/api/v1/fitness/plan-configs", async (int? goal, string? status, int? page, int? pageSize, ISender sender) =>
            {
                var result = await sender.Send(new GetPlanConfigsQuery(goal ?? 0, status ?? "", page ?? 1, pageSize ?? 20));
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("GetPlanConfigs").WithTags("Fitness").RequireAuthorization();

            app.MapGet("/api/v1/fitness/plans/{planId}", async (Guid planId, ISender sender) =>
            {
                var result = await sender.Send(new GetPlanDetailQuery(planId));
                return result is null ? Results.NotFound(new { message = "Plan not found" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("GetPlanDetail").WithTags("Fitness").RequireAuthorization();
        }
    }
}
