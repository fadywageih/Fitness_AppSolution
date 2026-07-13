using WorkoutCatalog.Domain.Enums;

namespace WorkoutCatalog.Domain.Entities
{

    public class Workout
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public WorkoutCategory Category { get; private set; }
        public DifficultyLevel Difficulty { get; private set; }
        public int DurationMinutes { get; private set; }
        public int OrderIndex { get; private set; }
        public Guid? PlanId { get; private set; }

        public WorkoutPlan? Plan { get; private set; }
        public List<WorkoutExercise> WorkoutExercises { get; private set; } = new();

        private Workout() { }

        public Workout(string name, string description, WorkoutCategory category, DifficultyLevel difficulty, int durationMinutes, int orderIndex = 1, Guid? planId = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Category = category;
            Difficulty = difficulty;
            DurationMinutes = durationMinutes;
            OrderIndex = orderIndex;
            PlanId = planId;
        }
    }

}
