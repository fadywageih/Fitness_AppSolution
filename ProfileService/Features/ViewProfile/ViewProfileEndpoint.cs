using MediatR;

namespace ProfileService.Features.ViewProfile
{
    public static class ViewProfileEndpoint
    {
        public static void MapViewProfileEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/profile", async (ISender sender) =>
            {
                var result = await sender.Send(new ViewProfileQuery());

                if (result is null)
                    return Results.NotFound(new { message = "Profile not found" });

                return Results.Ok(new { isSuccess = true, message = "Success", data = result, statusCode = 200 });
            })
            .WithName("ViewProfile")
            .WithTags("Profile")
            .RequireAuthorization();
        }
    }
}
