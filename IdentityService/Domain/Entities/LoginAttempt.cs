namespace IdentityService.Domain.Entities
{
    public class LoginAttempt
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime AttemptedAt { get; private set; }
        public bool IsSuccess { get; private set; }
        public string? FailureReason { get; private set; }
        public string? IpAddress { get; private set; }

        public User User { get; private set; } = null!;

        private LoginAttempt() { }

        private LoginAttempt(Guid userId, bool isSuccess, string? failureReason, string? ipAddress)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            AttemptedAt = DateTime.UtcNow;
            IsSuccess = isSuccess;
            FailureReason = failureReason;
            IpAddress = ipAddress;
        }

        public static LoginAttempt CreateSuccess(Guid userId, string? ipAddress = null)
            => new(userId, true, null, ipAddress);

        public static LoginAttempt CreateFailure(Guid userId, string failureReason, string? ipAddress = null)
            => new(userId, false, failureReason, ipAddress);
    }
}
