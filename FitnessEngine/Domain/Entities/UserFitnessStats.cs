using FitnessEngine.Domain.Enums;

namespace FitnessEngine.Domain.Entities
{
    public class UserFitnessStats
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Weight { get; private set; }
        public decimal Height { get; private set; }
        public int Age { get; private set; }
        public string Gender { get; private set; } = string.Empty;
        public FitnessGoal Goal { get; private set; }
        public ActivityLevel ActivityLevel { get; private set; }
        public DateTime RecordedAt { get; private set; }

        private UserFitnessStats() { }

        private UserFitnessStats(Guid userId, decimal weight, decimal height, int age, string gender, FitnessGoal goal, ActivityLevel activityLevel)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Weight = weight;
            Height = height;
            Age = age;
            Gender = gender;
            Goal = goal;
            ActivityLevel = activityLevel;
            RecordedAt = DateTime.UtcNow;
        }

        public static UserFitnessStats Create(Guid userId, decimal weight, decimal height, int age, string gender, FitnessGoal goal, ActivityLevel activityLevel)
            => new(userId, weight, height, age, gender, goal, activityLevel);
    }
}
