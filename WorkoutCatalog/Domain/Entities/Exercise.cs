namespace WorkoutCatalog.Domain.Entities
{

    public class Exercise
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string TargetMuscles { get; private set; } = string.Empty;
        public string Equipment { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string? VideoUrl { get; private set; }
        public string? ImageUrl { get; private set; }

        private Exercise() { }

        public Exercise(string name, string targetMuscles, string equipment, string description, string? videoUrl = null, string? imageUrl = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            TargetMuscles = targetMuscles;
            Equipment = equipment;
            Description = description;
            VideoUrl = videoUrl;
            ImageUrl = imageUrl;
        }
    }
}
