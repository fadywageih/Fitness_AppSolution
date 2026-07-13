namespace SharedContracts.Events
{
    public record SubscriptionUpgradedEvent(Guid UserId, string Tier, DateTime ExpiresAt);

}
