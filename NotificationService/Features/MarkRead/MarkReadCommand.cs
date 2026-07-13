using MediatR;

namespace NotificationService.Features.MarkRead
{

    public record MarkReadCommand(Guid Id) : IRequest<bool>;
}
