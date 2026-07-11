using FitnessEngine.Domain.Enums;

namespace FitnessEngine.Domain.Entities
{
    public class CalculatedMetrics
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Bmr { get; private set; }
        public decimal Tdee { get; private set; }
        public decimal CalorieTarget { get; private set; }
        public FitnessStatus Status { get; private set; }
        public DateTime CalculatedAt { get; private set; }

        private CalculatedMetrics() { }

        private CalculatedMetrics(Guid userId, decimal bmr, decimal tdee, decimal calorieTarget, FitnessStatus status)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Bmr = bmr;
            Tdee = tdee;
            CalorieTarget = calorieTarget;
            Status = status;
            CalculatedAt = DateTime.UtcNow;
        }

        public static CalculatedMetrics Create(Guid userId, decimal bmr, decimal tdee, decimal calorieTarget, FitnessStatus status)
            => new(userId, bmr, tdee, calorieTarget, status);

        public void Update(decimal bmr, decimal tdee, decimal calorieTarget, FitnessStatus status)
        {
            Bmr = bmr;
            Tdee = tdee;
            CalorieTarget = calorieTarget;
            Status = status;
            CalculatedAt = DateTime.UtcNow;
        }
    }

}
