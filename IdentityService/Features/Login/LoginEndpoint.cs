using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.Login
{
    public static class LoginEndpoint
    {
        public static void MapLoginEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/auth/login", async (
                string email,
                string password,
                ISender sender) =>
            {
                var validator = new LoginCommandValidator();
                var command = new LoginCommand(email, password);
                var validationResult = await validator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors
                        .Select(e => e.ErrorMessage)
                        .ToArray();
                    return Results.Json(
                        AuthResponse.Failure("Validation failed", 400, errors),
                        statusCode: 400);
                }

                var result = await sender.Send(command);
                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithName("Login")
            .WithTags("Authentication");
        }
    }
}
