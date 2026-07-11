namespace SharedContracts.Events
{
    public record WeightUpdatedEvent(Guid UserId, decimal NewWeight);
}
