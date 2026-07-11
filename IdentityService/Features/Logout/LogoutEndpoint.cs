using MediatR;

namespace IdentityService.Features.Logout
{
    public static class LogoutEndpoint
    {
        public static void MapLogoutEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/auth/logout", async (ISender sender) =>
            {
                var command = new LogoutCommand();
                var result = await sender.Send(command);
                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithName("Logout")
            .WithTags("Authentication")
            .RequireAuthorization();
        }
    }
}
