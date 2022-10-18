namespace Ioco.Infrastructure.Cache.Interfaces;

public interface ICacheProvider
{
    Task<ICacheResult<T?>> Get<T>(string key, CancellationToken token = default);

    Task Set<T>(string key, T data, TimeSpan cacheDuration = default, CancellationToken token = default);
}
