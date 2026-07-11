using MediatR;

namespace ProfileService.Features.UpdateProfile
{
    public static class UpdateProfileEndpoint
    {
        public static void MapUpdateProfileEndpoint(this WebApplication app)
        {
            app.MapPut("/api/v1/profile", async (
                string firstName,
                string lastName,
                string email,
                string phoneNumber,
                ISender sender) =>
            {
                var validator = new UpdateProfileCommandValidator();
                var command = new UpdateProfileCommand(firstName, lastName, email, phoneNumber);
                var validationResult = await validator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                    return Results.BadRequest(new { isSuccess = false, message = "Validation failed", errors, statusCode = 400 });
                }

                var result = await sender.Send(command);

                if (result is null)
                    return Results.Unauthorized();

                return Results.Ok(new { isSuccess = true, message = "Profile updated", data = result, statusCode = 200 });
            })
            .WithName("UpdateProfile")
            .WithTags("Profile")
            .RequireAuthorization();
        }
    }
}
