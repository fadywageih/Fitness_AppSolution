using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.Register;

public static class RegisterEndpoint
{
    public static void MapRegisterEndpoint(this WebApplication app)
    {
        app.MapPost("/api/v1/auth/register", async (
            string firstName,
            string lastName,
            string email,
            string password,
            string phoneNumber,
            ISender sender) =>
        {
            // Manual validation
            var validator = new RegisterCommandValidator();
            var command = new RegisterCommand(firstName, lastName, email, password, phoneNumber);
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
        .WithName("Register")
        .WithTags("Authentication");
    }
}