using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.VerifyOtp
{
    public static class VerifyOtpEndpoint
    {
        public static void MapVerifyOtpEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/auth/verify-otp", async (
                string email,
                string otp,
                ISender sender) =>
            {
                var validator = new VerifyOtpCommandValidator();
                var command = new VerifyOtpCommand(email, otp);
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
            .WithName("VerifyOtp")
            .WithTags("Authentication");
        }
    }

}
