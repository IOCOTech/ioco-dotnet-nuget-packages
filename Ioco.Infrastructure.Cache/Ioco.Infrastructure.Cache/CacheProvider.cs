using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ioco.Infrastructure.Cache;
using Ioco.Infrastructure.Cache.Entities;
using Ioco.Infrastructure.Cache.Interfaces;
using System.Text.Json;

namespace Ioco.Infrastructure.Interfaces.Cache;

/// <summary>
/// Basic cache provider for distributed caching
/// </summary>
public class CacheProvider : ICacheProvider
{
    private IDistributedCache DistributedCache { get; }
    private CacheSettings CacheConfig { get; }

    private const string NULLSTRING = "null";
    private const string PREFIX = "Ioco";

    public CacheProvider(IDistributedCache distributedCache, IOptions<CacheSettings> options, ILogger<CacheProvider> logger)
    {
        DistributedCache = distributedCache;
        CacheConfig = options.Value;

        logger.LogInformation("Initializing CacheProvider");
    }

    /// <summary>
    /// Fetch a stored item from cache for the specified key
    /// </summary>
    /// <typeparam name="T">Item fetched from cache</typeparam>
    /// <param name="key">Cache Key</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns></returns>
    public async Task<ICacheResult<T?>> Get<T>(string key, CancellationToken token = default)
    {
        var data = await DistributedCache.GetStringAsync($"{PREFIX}.{key}", token);

        if (data == null)
        {
            // data was not found in cache at all
            return new CacheMissResult<T?>();
        }

        // Data exists in cache but it was set to null to prevent load spam
        if (data == NULLSTRING)
        {
            // return cache hit but set data to null
            return new CacheNullHitResult<T?>();
        }

        // return cache hit
        return new CacheHitResult<T?>(JsonSerializer.Deserialize<T>(data));
    }

    /// <summary>
    /// Store a data item in Cache under the given cache key
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">Cache Key</param>
    /// <param name="data">Data to store</param>
    /// <param name="cacheDuration">The duration which the item will stay in cache</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns></returns>
    public async Task Set<T>(string key, T data, TimeSpan cacheDuration = default, CancellationToken token = default)
    {
        var cacheOptions = new DistributedCacheEntryOptions();

        if (data == null)
        {
            cacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(CacheConfig.NullCacheDurationInSeconds);

            await DistributedCache.SetStringAsync($"{PREFIX}.{key}", NULLSTRING, cacheOptions, token);
            return;
        }

        cacheOptions.AbsoluteExpirationRelativeToNow = cacheDuration == default ?
            TimeSpan.FromSeconds(CacheConfig.DefaultCacheDurationInSeconds)
            : cacheDuration;

        var serializedData = JsonSerializer.Serialize(data);

        await DistributedCache.SetStringAsync($"{PREFIX}.{key}", serializedData, cacheOptions, token);
    }


}
