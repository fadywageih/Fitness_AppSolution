using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.ChangePassword
{
    public static class ChangePasswordEndpoint
    {
        public static void MapChangePasswordEndpoint(this WebApplication app)
        {
            app.MapPut("/api/v1/profile/change-password", async (
                string currentPassword,
                string newPassword,
                string confirmPassword,
                ISender sender) =>
            {
                var validator = new ChangePasswordCommandValidator();
                var command = new ChangePasswordCommand(currentPassword, newPassword, confirmPassword);
                var validationResult = await validator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                    return Results.Json(AuthResponse.Failure("Validation failed", 400, errors), statusCode: 400);
                }

                var result = await sender.Send(command);
                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithName("ChangePassword")
            .WithTags("Profile")
            .RequireAuthorization();
        }
    }
}
