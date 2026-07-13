using MediatR;

namespace WorkoutCatalog.Features.BrowsePlans
{


public static class BrowsePlansEndpoint
{
    public static void MapBrowsePlansEndpoint(this WebApplication app)
    {
        app.MapGet("/api/v1/workout-plans", async (int? page, int? pageSize, ISender sender) =>
        {
            var result = await sender.Send(new BrowsePlansQuery(page ?? 1, pageSize ?? 20));
            return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
        }).WithName("BrowsePlans").WithTags("Plans").RequireAuthorization();
    }
}
}
