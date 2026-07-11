using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.ForgotPassword
{
    public static class ForgotPasswordEndpoint
    {
        public static void MapForgotPasswordEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/auth/forgot-password", async (
                string email,
                ISender sender) =>
            {
                var validator = new ForgotPasswordCommandValidator();
                var command = new ForgotPasswordCommand(email);
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
            .WithName("ForgotPassword")
            .WithTags("Authentication");
        }
    }
}
