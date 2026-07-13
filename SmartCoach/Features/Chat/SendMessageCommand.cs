using MediatR;

namespace SmartCoach.Features.Chat
{

    public record SendMessageCommand(string Message, Guid? SessionId) : IRequest<ChatResponse>;

    public record ChatResponse(Guid SessionId, string Reply, string[]? FollowUpSuggestions);

}
