namespace Ioco.Infrastructure.Crypto.JWT;

public class PasswordHasher : IPasswordHasher
{
    public Task<string> Hash(string password)
    {
        // BCrypt doesnt support async
        return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
    }

    public Task<bool> Verify(string password, string passwordHash)
    {
        // BCrypt doesnt support async
        return Task.FromResult(BCrypt.Net.BCrypt.Verify(password, passwordHash));
    }
}
