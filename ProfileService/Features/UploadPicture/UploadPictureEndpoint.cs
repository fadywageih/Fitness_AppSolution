using MediatR;

namespace ProfileService.Features.UploadPicture
{
    public static class UploadPictureEndpoint
    {
        public static void MapUploadPictureEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/profile/picture", async (
                IFormFile profilePicture,
                ISender sender) =>
            {
                if (profilePicture is null)
                    return Results.BadRequest(new { isSuccess = false, message = "File is required", statusCode = 400 });

                var command = new UploadPictureCommand(profilePicture);
                var result = await sender.Send(command);

                if (result is null)
                    return Results.BadRequest(new { isSuccess = false, message = "Upload failed", statusCode = 400 });

                return Results.Ok(new { isSuccess = true, message = "Picture uploaded", data = result, statusCode = 200 });
            })
            .WithName("UploadPicture")
            .WithTags("Profile")
            .RequireAuthorization()
            .DisableAntiforgery();
        }
    }
}
