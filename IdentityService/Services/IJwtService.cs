namespace IdentityService.Services
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string email, bool isPremium);
    }
}
