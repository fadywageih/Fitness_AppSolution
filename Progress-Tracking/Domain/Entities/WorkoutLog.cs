namespace Progress_Tracking.Domain.Entities
{

    public class WorkoutLog
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid WorkoutId { get; private set; }
        public Guid SessionId { get; private set; }
        public DateTime CompletedAt { get; private set; }
        public int DurationMinutes { get; private set; }
        public int CaloriesBurned { get; private set; }
        public string? Difficulty { get; private set; }
        public string? Notes { get; private set; }
        public int Rating { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public List<WorkoutLogExercise> Exercises { get; private set; } = new();

        private WorkoutLog() { }

        private WorkoutLog(Guid userId, Guid workoutId, Guid sessionId, DateTime completedAt, int durationMinutes, int caloriesBurned, string? difficulty, string? notes, int rating)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            WorkoutId = workoutId;
            SessionId = sessionId;
            CompletedAt = completedAt;
            DurationMinutes = durationMinutes;
            CaloriesBurned = caloriesBurned;
            Difficulty = difficulty;
            Notes = notes;
            Rating = rating;
            CreatedAt = DateTime.UtcNow;
        }

        public static WorkoutLog Create(Guid userId, Guid workoutId, Guid sessionId, DateTime completedAt, int durationMinutes, int caloriesBurned, string? difficulty, string? notes, int rating)
            => new(userId, workoutId, sessionId, completedAt, durationMinutes, caloriesBurned, difficulty, notes, rating);
    }

}
