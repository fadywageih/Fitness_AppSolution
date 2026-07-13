using MediatR;

namespace SmartCoach.Features.Chat
{

    public static class ChatEndpoint
    {
        public static void MapChatEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/smart-coach/chat", async (string message, Guid? sessionId, ISender sender) =>
            {
                var result = await sender.Send(new SendMessageCommand(message, sessionId));
                return Results.Ok(new { isSuccess = true, data = result, statusCode = 200 });
            }).WithName("Chat").WithTags("SmartCoach").RequireAuthorization();
        }
    }

}
