namespace Ioco.Infrastructure.Crypto;

public interface IJWTProvider
{
    Task<string> CreateToken(Dictionary<string, object> claims, int? minutesToExpire = null);
    Task<string> DecodeToken(string token);
}
