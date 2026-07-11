using MediatR;

namespace ProfileService.Features.ViewSettings
{
    public record ViewSettingsQuery : IRequest<ViewSettingsResponse?>;

    public record ViewSettingsResponse(
        string Theme,
        string Language,
        bool WorkoutReminders
    );
}
