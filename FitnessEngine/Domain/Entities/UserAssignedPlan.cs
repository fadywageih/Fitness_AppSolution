namespace FitnessEngine.Domain.Entities
{

    public class UserAssignedPlan
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid PlanConfigId { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime AssignedAt { get; private set; }
        public DateTime? DeactivatedAt { get; private set; }

        public FitnessPlanConfig PlanConfig { get; private set; } = null!;

        private UserAssignedPlan() { }

        private UserAssignedPlan(Guid userId, Guid planConfigId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            PlanConfigId = planConfigId;
            IsActive = true;
            AssignedAt = DateTime.UtcNow;
        }

        public static UserAssignedPlan Create(Guid userId, Guid planConfigId)
            => new(userId, planConfigId);

        public void Deactivate()
        {
            IsActive = false;
            DeactivatedAt = DateTime.UtcNow;
        }
    }

}
