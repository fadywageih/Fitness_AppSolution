using MediatR;

namespace ProfileService.Features.ViewProfile
{
    public record ViewProfileQuery : IRequest<ViewProfileResponse>;

    public record ViewProfileResponse(
        Guid UserId,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string? ProfilePictureUrl,
        string Theme,
        string Language,
        bool WorkoutReminders
    );
}
