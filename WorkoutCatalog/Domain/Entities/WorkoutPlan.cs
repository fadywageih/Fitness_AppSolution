using WorkoutCatalog.Domain.Enums;

namespace WorkoutCatalog.Domain.Entities;

public class WorkoutPlan
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DifficultyLevel Difficulty { get; private set; }
    public int DurationWeeks { get; private set; }
    public List<Workout> Workouts { get; private set; } = new();

    private WorkoutPlan() { }

    public WorkoutPlan(string name, string description, DifficultyLevel difficulty, int durationWeeks)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Difficulty = difficulty;
        DurationWeeks = durationWeeks;
    }
}