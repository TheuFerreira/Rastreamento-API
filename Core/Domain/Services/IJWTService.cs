namespace Core.Domain.Services
{
    public interface IJWTService
    {
        string BuildToken(DateTime expiresAt, int userId, byte[] key);
        int ValidateToken(string token, byte[] key);
    }
}
