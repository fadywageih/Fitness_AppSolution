namespace Progress_Tracking.Domain.Entities
{

    public class WorkoutLogExercise
    {
        public Guid Id { get; private set; }
        public Guid WorkoutLogId { get; private set; }
        public Guid ExerciseId { get; private set; }
        public int SetsCompleted { get; private set; }
        public int RepsCompleted { get; private set; }

        public WorkoutLog WorkoutLog { get; private set; } = null!;

        private WorkoutLogExercise() { }

        public WorkoutLogExercise(Guid workoutLogId, Guid exerciseId, int setsCompleted, int repsCompleted)
        {
            Id = Guid.NewGuid();
            WorkoutLogId = workoutLogId;
            ExerciseId = exerciseId;
            SetsCompleted = setsCompleted;
            RepsCompleted = repsCompleted;
        }
    }
}
