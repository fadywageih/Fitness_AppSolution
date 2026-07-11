using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.ResetPassword
{
    public static class ResetPasswordEndpoint
    {
        public static void MapResetPasswordEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/auth/reset-password", async (
                string resetToken,
                string newPassword,
                string confirmPassword,
                ISender sender) =>
            {
                var validator = new ResetPasswordCommandValidator();
                var command = new ResetPasswordCommand(resetToken, newPassword, confirmPassword);
                var validationResult = await validator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                    return Results.Json(
                        AuthResponse.Failure("Validation failed", 400, errors),
                        statusCode: 400);
                }

                var result = await sender.Send(command);
                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithName("ResetPassword")
            .WithTags("Authentication");
        }
    }

}
