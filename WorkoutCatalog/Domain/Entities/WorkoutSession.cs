namespace WorkoutCatalog.Domain.Entities
{

    public class WorkoutSession
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid WorkoutId { get; private set; }
        public string Status { get; private set; } = "Active";
        public DateTime StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        public Workout Workout { get; private set; } = null!;

        private WorkoutSession() { }

        private WorkoutSession(Guid userId, Guid workoutId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            WorkoutId = workoutId;
            StartedAt = DateTime.UtcNow;
        }

        public static WorkoutSession Create(Guid userId, Guid workoutId)
            => new(userId, workoutId);

        public void Complete() => Status = "Completed";
    }

}
