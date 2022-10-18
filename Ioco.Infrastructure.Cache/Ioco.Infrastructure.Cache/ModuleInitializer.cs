using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ioco.Infrastructure.Cache.Interfaces;
using Ioco.Infrastructure.Interfaces.Cache;


namespace Ioco.Infrastructure.Cache
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddCacheSupport(this IServiceCollection serviceCollection, IConfiguration config, string environment)
        {
            var configSection = config.GetSection(CacheSettings.ConfigSectionName);
            var cacheConfig = configSection.Get<CacheSettings>();

            serviceCollection.Configure<CacheSettings>(configSection);


            serviceCollection.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config.GetConnectionString("Redis");
                options.InstanceName = $"{cacheConfig.InstanceName}.{environment}";
            });


            serviceCollection.AddSingleton<ICacheProvider, CacheProvider>();

            return serviceCollection;
        }
    }
}
