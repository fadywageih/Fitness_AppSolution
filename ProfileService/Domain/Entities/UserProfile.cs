namespace ProfileService.Domain.Entities
{
    public class UserProfile
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string? ProfilePictureUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private UserProfile() { }

        private UserProfile(Guid userId, string firstName, string lastName, string email, string phoneNumber)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            CreatedAt = DateTime.UtcNow;
        }

        public static UserProfile Create(Guid userId, string firstName, string lastName, string email, string phoneNumber)
            => new(userId, firstName, lastName, email, phoneNumber);

        public void Update(string firstName, string lastName, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePicture(string pictureUrl)
        {
            ProfilePictureUrl = pictureUrl;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
