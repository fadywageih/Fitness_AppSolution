using MediatR;

namespace ProfileService.Features.UpdateSettings
{
    public static class UpdateSettingsEndpoint
    {
        public static void MapUpdateSettingsEndpoint(this WebApplication app)
        {
            app.MapPut("/api/v1/settings", async (
                string? theme,
                string? language,
                bool? workoutReminders,
                ISender sender) =>
            {
                var command = new UpdateSettingsCommand(theme, language, workoutReminders);
                var result = await sender.Send(command);

                if (result is null)
                    return Results.Unauthorized();

                return Results.Ok(new { isSuccess = true, message = "Settings updated", data = result, statusCode = 200 });
            })
            .WithName("UpdateSettings")
            .WithTags("Settings")
            .RequireAuthorization();
        }
    }
}
