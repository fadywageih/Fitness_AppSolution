namespace IdentityService.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool RequiresProfileCompletion { get; private set; }
        public bool IsLockedOut { get; private set; }
        public DateTime? LockedUntil { get; private set; }
        public bool IsPremium { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private User() { }

        private User(
            string firstName,
            string lastName,
            string email,
            string passwordHash,
            string phoneNumber)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            RequiresProfileCompletion = true;
            IsLockedOut = false;
            IsPremium = false;
            CreatedAt = DateTime.UtcNow;
        }

        public static User Create(
            string firstName,
            string lastName,
            string email,
            string passwordHash,
            string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2 || firstName.Length > 50)
                throw new ArgumentException("First name must be between 2 and 50 characters");

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2 || lastName.Length > 50)
                throw new ArgumentException("Last name must be between 2 and 50 characters");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("Invalid email format");

            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length != 11 || !phoneNumber.StartsWith("01"))
                throw new ArgumentException("Invalid Egyptian phone number");

            return new User(firstName, lastName, email.ToLowerInvariant(), passwordHash, phoneNumber);
        }

        public void CompleteProfile()
        {
            RequiresProfileCompletion = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void LockAccount(int lockDurationMinutes = 15)
        {
            IsLockedOut = true;
            LockedUntil = DateTime.UtcNow.AddMinutes(lockDurationMinutes);
            UpdatedAt = DateTime.UtcNow;
        }

        public void UnlockAccount()
        {
            IsLockedOut = false;
            LockedUntil = null;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool IsLockoutActive()
        {
            if (!IsLockedOut || LockedUntil == null)
                return false;

            if (DateTime.UtcNow > LockedUntil.Value)
            {
                UnlockAccount();
                return false;
            }

            return true;
        }

        public void UpdatePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPremium(bool isPremium)
        {
            IsPremium = isPremium;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
