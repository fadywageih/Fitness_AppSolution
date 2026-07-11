using MediatR;

namespace ProfileService.Features.ViewSettings
{
    public static class ViewSettingsEndpoint
    {
        public static void MapViewSettingsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/settings", async (ISender sender) =>
            {
                var result = await sender.Send(new ViewSettingsQuery());

                if (result is null)
                    return Results.NotFound(new { message = "Settings not found" });

                return Results.Ok(new { isSuccess = true, message = "Success", data = result, statusCode = 200 });
            })
            .WithName("ViewSettings")
            .WithTags("Settings")
            .RequireAuthorization();
        }
    }

}
