using MediatR;

namespace IdentityService.Features.CompleteProfile
{
    public static class CompleteProfileEndpoint
    {
        public static void MapCompleteProfileEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/auth/complete-profile", async (
                ISender sender) =>
            {
                var command = new CompleteProfileCommand();
                var result = await sender.Send(command);
                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithName("CompleteProfile")
            .WithTags("Authentication")
            .RequireAuthorization();
        }
    }
}
