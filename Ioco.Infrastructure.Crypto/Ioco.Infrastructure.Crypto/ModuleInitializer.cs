using Microsoft.Extensions.DependencyInjection;
using Ioco.Infrastructure.Crypto.JWT;

namespace Ioco.Infrastructure.Crypto
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddCryptoSupport(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPasswordHasher, PasswordHasher>();
            serviceCollection.AddScoped<IJWTProvider, JWTProvider>();

            return serviceCollection;
        }
    }
}
