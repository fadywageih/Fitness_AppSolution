using MediatR;

namespace NotificationService.Features.GetNotifications
{

    public record GetNotificationsQuery : IRequest<List<NotificationResponse>>;

    public record NotificationResponse(Guid Id, string Title, string Message, string? IconUrl, bool IsRead, DateTime CreatedAt);
}
