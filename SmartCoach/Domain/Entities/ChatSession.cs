namespace SmartCoach.Domain.Entities
{

    public class ChatSession
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public List<ChatMessage> Messages { get; private set; } = new();

        private ChatSession() { }

        private ChatSession(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        public static ChatSession Create(Guid userId) => new(userId);
    }
}
