namespace SmartCoach.Domain.Entities
{

    public class ChatMessage
    {
        public Guid Id { get; private set; }
        public Guid SessionId { get; private set; }
        public string Sender { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;
        public DateTime SentAt { get; private set; }

        public ChatSession Session { get; private set; } = null!;

        private ChatMessage() { }

        public ChatMessage(Guid sessionId, string sender, string content)
        {
            Id = Guid.NewGuid();
            SessionId = sessionId;
            Sender = sender;
            Content = content;
            SentAt = DateTime.UtcNow;
        }
    }
}
