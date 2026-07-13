namespace SubscriptionService.Domain.Entities
{

    public class UserSubscription
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Tier { get; private set; } = "Free";
        public string Status { get; private set; } = "Active";
        public DateTime? ExpiresAt { get; private set; }
        public bool AutoRenew { get; private set; } = true;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private UserSubscription() { }

        private UserSubscription(Guid userId, string tier, DateTime? expiresAt)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Tier = tier;
            ExpiresAt = expiresAt;
            CreatedAt = DateTime.UtcNow;
        }

        public static UserSubscription Create(Guid userId, string tier = "Free", DateTime? expiresAt = null)
            => new(userId, tier, expiresAt);

        public void Upgrade(string tier, DateTime expiresAt)
        {
            Tier = tier;
            ExpiresAt = expiresAt;
            Status = "Active";
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            AutoRenew = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
