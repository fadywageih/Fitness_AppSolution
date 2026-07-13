namespace Progress_Tracking.Domain.Entities
{

    public class UserStatistics
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public int TotalWorkouts { get; private set; }
        public int TotalCaloriesBurned { get; private set; }
        public int CurrentStreak { get; private set; }
        public int LongestStreak { get; private set; }
        public DateTime? LastWorkoutDate { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private UserStatistics() { }

        private UserStatistics(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            UpdatedAt = DateTime.UtcNow;
        }

        public static UserStatistics Create(Guid userId) => new(userId);

        public void AddWorkout(int caloriesBurned, DateTime date)
        {
            TotalWorkouts++;
            TotalCaloriesBurned += caloriesBurned;

            if (LastWorkoutDate.HasValue && date.Date == LastWorkoutDate.Value.Date.AddDays(1))
                CurrentStreak++;
            else if (!LastWorkoutDate.HasValue || date.Date > LastWorkoutDate.Value.Date.AddDays(1))
                CurrentStreak = 1;

            if (CurrentStreak > LongestStreak) LongestStreak = CurrentStreak;
            LastWorkoutDate = date;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
