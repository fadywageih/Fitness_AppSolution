namespace IdentityService.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string TokenHash { get; private set; } = string.Empty;
        public DateTime ExpiresAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public string? ReplacedByToken { get; private set; }

        public bool IsRevoked => RevokedAt != null;
        public bool IsExpired => DateTime.UtcNow > ExpiresAt;
        public bool IsActive => !IsRevoked && !IsExpired;

        public User User { get; private set; } = null!;

        private RefreshToken() { }

        private RefreshToken(Guid userId, string tokenHash, DateTime expiresAt)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            TokenHash = tokenHash;
            ExpiresAt = expiresAt;
            CreatedAt = DateTime.UtcNow;
        }

        public static RefreshToken Create(Guid userId, string tokenHash, int expiryDays = 7)
            => new(userId, tokenHash, DateTime.UtcNow.AddDays(expiryDays));

        public void Revoke(string? replacedByToken = null)
        {
            RevokedAt = DateTime.UtcNow;
            ReplacedByToken = replacedByToken;
        }
    }
}
