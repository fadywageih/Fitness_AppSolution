using MediatR;

namespace ProfileService.Features.UpdateSettings
{
    public record UpdateSettingsCommand(
        string? Theme,
        string? Language,
        bool? WorkoutReminders
    ) : IRequest<UpdateSettingsResponse?>;

    public record UpdateSettingsResponse(
        string Theme,
        string Language,
        bool WorkoutReminders
    );
}
