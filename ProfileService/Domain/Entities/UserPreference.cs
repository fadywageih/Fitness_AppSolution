namespace ProfileService.Domain.Entities
{
    public class UserPreference
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Theme { get; private set; } = "light";
        public string Language { get; private set; } = "en";
        public bool WorkoutReminders { get; private set; } = true;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private UserPreference() { }

        private UserPreference(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        public static UserPreference Create(Guid userId) => new(userId);

        public void UpdateTheme(string theme)
        {
            Theme = theme;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateLanguage(string language)
        {
            Language = language;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateWorkoutReminders(bool enabled)
        {
            WorkoutReminders = enabled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
