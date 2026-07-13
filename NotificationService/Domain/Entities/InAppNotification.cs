namespace NotificationService.Domain.Entities
{

    public class InAppNotification
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        public string? IconUrl { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private InAppNotification() { }

        public InAppNotification(Guid userId, string title, string message, string? iconUrl = null)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Title = title;
            Message = message;
            IconUrl = iconUrl;
            IsRead = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsRead() => IsRead = true;
    }
}
