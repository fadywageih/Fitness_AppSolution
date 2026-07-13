using MediatR;
using SmartCoach.Domain.Entities;
using SmartCoach.Persistence;
using System.Security.Claims;

namespace SmartCoach.Features.Chat;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ChatResponse>
{
    private readonly SmartCoachDbContext _context;
    private readonly IHttpContextAccessor _http;

    public SendMessageCommandHandler(SmartCoachDbContext context, IHttpContextAccessor http)
    {
        _context = context;
        _http = http;
    }

    public async Task<ChatResponse> Handle(SendMessageCommand command, CancellationToken ct)
    {
        var userIdClaim = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.TryParse(userIdClaim, out var uid) ? uid : Guid.NewGuid();

        ChatSession session;

        if (command.SessionId.HasValue)
        {
            session = await _context.Sessions.FindAsync(new object[] { command.SessionId.Value }, ct);
            if (session is null)
            {
                session = ChatSession.Create(userId);
                _context.Sessions.Add(session);
                await _context.SaveChangesAsync(ct);
            }
        }
        else
        {
            session = ChatSession.Create(userId);
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync(ct);
        }

        var userMsg = new ChatMessage(session.Id, "User", command.Message);
        _context.Messages.Add(userMsg);

        var reply = GetAiReply(command.Message);
        var aiMsg = new ChatMessage(session.Id, "AI", reply);
        _context.Messages.Add(aiMsg);

        await _context.SaveChangesAsync(ct);

        return new ChatResponse(session.Id, reply, new[] { "Tell me about workouts", "What should I eat today?", "How's my progress?" });
    }

    private string GetAiReply(string message)
    {
        var lower = message.ToLower();
        if (lower.Contains("workout") || lower.Contains("exercise"))
            return "Based on your fitness plan, I recommend focusing on chest and arms today. Would you like me to suggest specific exercises?";
        if (lower.Contains("eat") || lower.Contains("food") || lower.Contains("meal"))
            return "Based on your calorie target, I recommend a protein-rich meal like Grilled Chicken Breast. Would you like to see meal options?";
        if (lower.Contains("progress") || lower.Contains("weight"))
            return "You're doing great! You've completed 1 workout and logged your weight. Keep going!";
        return "I'm your fitness AI coach! Ask me about workouts, nutrition, or your progress. How can I help you today?";
    }
}