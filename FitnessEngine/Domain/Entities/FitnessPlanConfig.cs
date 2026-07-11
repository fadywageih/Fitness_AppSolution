using FitnessEngine.Domain.Enums;

namespace FitnessEngine.Domain.Entities
{

    public class FitnessPlanConfig
    {
        public Guid Id { get; private set; }
        public FitnessGoal Goal { get; private set; }
        public FitnessStatus Status { get; private set; }
        public string PlanName { get; private set; } = string.Empty;
        public int DurationWeeks { get; private set; }
        public int WorkoutsPerWeek { get; private set; }
        public string Description { get; private set; } = string.Empty;

        private FitnessPlanConfig() { }

        public FitnessPlanConfig(FitnessGoal goal, FitnessStatus status, string planName, int durationWeeks, int workoutsPerWeek, string description)
        {
            Id = Guid.NewGuid();
            Goal = goal;
            Status = status;
            PlanName = planName;
            DurationWeeks = durationWeeks;
            WorkoutsPerWeek = workoutsPerWeek;
            Description = description;
        }
    }
}
