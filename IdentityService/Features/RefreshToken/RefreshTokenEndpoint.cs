using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.RefreshToken
{
    public static class RefreshTokenEndpoint
    {
        public static void MapRefreshTokenEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/auth/refresh-token", async (
                string refreshToken,
                ISender sender) =>
            {
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return Results.Json(
                        AuthResponse.Failure("Refresh token is required", 400),
                        statusCode: 400);
                }

                var command = new RefreshTokenCommand(refreshToken);
                var result = await sender.Send(command);
                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithName("RefreshToken")
            .WithTags("Authentication");
        }
    }
}
