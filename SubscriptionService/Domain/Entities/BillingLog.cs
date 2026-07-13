namespace SubscriptionService.Domain.Entities
{

    public class BillingLog
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string PlanTier { get; private set; } = string.Empty;
        public int DurationMonths { get; private set; }
        public decimal Amount { get; private set; }
        public string PaymentStatus { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }

        private BillingLog() { }

        private BillingLog(Guid userId, string planTier, int durationMonths, decimal amount, string paymentStatus)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            PlanTier = planTier;
            DurationMonths = durationMonths;
            Amount = amount;
            PaymentStatus = paymentStatus;
            CreatedAt = DateTime.UtcNow;
        }

        public static BillingLog Create(Guid userId, string planTier, int durationMonths, decimal amount, string paymentStatus)
            => new(userId, planTier, durationMonths, amount, paymentStatus);
    }
}
