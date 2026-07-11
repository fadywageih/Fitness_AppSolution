namespace IdentityService.Domain.Entities;

public class OtpCode
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string CodeHash { get; private set; } = string.Empty;
    public string Purpose { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public bool IsUsed { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string? ResetToken { get; private set; }

    public User User { get; private set; } = null!;

    private OtpCode() { }

    private OtpCode(Guid userId, string codeHash, string purpose, DateTime expiresAt)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        CodeHash = codeHash;
        Purpose = purpose;
        ExpiresAt = expiresAt;
        IsUsed = false;
        CreatedAt = DateTime.UtcNow;
    }

    public static OtpCode Create(Guid userId, string codeHash, string purpose = "PasswordReset")
        => new(userId, codeHash, purpose, DateTime.UtcNow.AddMinutes(10));

    public bool IsExpired() => DateTime.UtcNow > ExpiresAt;

    public void MarkAsUsed() => IsUsed = true;

    public void SetResetToken(string resetToken) => ResetToken = resetToken;

    public void InvalidateResetToken() => ResetToken = null;
}