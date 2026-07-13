using MediatR;

namespace Progress_Tracking.Features.LogWeight
{

    public static class LogWeightEndpoint
    {
        public static void MapLogWeightEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/progress/weight", async (
                decimal weight, DateTime date, string? notes, ISender sender) =>
            {
                var validator = new LogWeightCommandValidator();
                var command = new LogWeightCommand(weight, date, notes);
                var vr = await validator.ValidateAsync(command);
                if (!vr.IsValid)
                    return Results.BadRequest(new { isSuccess = false, errors = vr.Errors.Select(e => e.ErrorMessage), statusCode = 400 });

                var result = await sender.Send(command);
                return result is null ? Results.Unauthorized()
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 201 });
            }).WithName("LogWeight").WithTags("Progress").RequireAuthorization();
        }
    }
}
