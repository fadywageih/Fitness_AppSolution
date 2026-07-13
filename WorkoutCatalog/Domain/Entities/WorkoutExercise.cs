namespace WorkoutCatalog.Domain.Entities
{

    public class WorkoutExercise
    {
        public Guid Id { get; private set; }
        public Guid WorkoutId { get; private set; }
        public Guid ExerciseId { get; private set; }
        public int Sets { get; private set; }
        public int Reps { get; private set; }
        public int? RestSeconds { get; private set; }
        public int OrderIndex { get; private set; }

        public Workout Workout { get; private set; } = null!;
        public Exercise Exercise { get; private set; } = null!;

        private WorkoutExercise() { }

        public WorkoutExercise(Guid workoutId, Guid exerciseId, int sets, int reps, int? restSeconds = null, int orderIndex = 1)
        {
            Id = Guid.NewGuid();
            WorkoutId = workoutId;
            ExerciseId = exerciseId;
            Sets = sets;
            Reps = reps;
            RestSeconds = restSeconds;
            OrderIndex = orderIndex;
        }
    }
}
