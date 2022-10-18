using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Microsoft.Extensions.Configuration;

namespace Ioco.Infrastructure.Crypto.JWT;

public class JWTProvider : IJWTProvider
{
    private readonly string Secret;
    private readonly IJwtEncoder Encoder;
    private readonly IJwtDecoder Decoder;

    public JWTProvider(IConfiguration config)
    {
        Secret = config["Crypto:UserJWTSecret"];
        IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
        IJsonSerializer serializer = new JsonNetSerializer();
        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        Encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
        IDateTimeProvider provider = new UtcDateTimeProvider();
        IJwtValidator validator = new JwtValidator(serializer, provider);
        Decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
    }

    public Task<string> CreateToken(Dictionary<string, object> claims, int? minutesToExpire = null)
    {
        if (minutesToExpire.HasValue)
        {
            claims.Add("exp", DateTimeOffset.UtcNow.AddMinutes(minutesToExpire.Value).ToUnixTimeSeconds());
        }

        var token = Encoder.Encode(claims, Secret);
        return Task.FromResult(token);
    }

    public Task<string> DecodeToken(string token)
    {
        try
        {
            var json = Decoder.Decode(token, Secret, verify: true);
            return Task.FromResult(json);
        }
        catch (TokenExpiredException)
        {
            return Task.FromResult("Token has expired");
        }
        catch (SignatureVerificationException)
        {
            return Task.FromResult("Token has invalid signature");
        }
    }
}
