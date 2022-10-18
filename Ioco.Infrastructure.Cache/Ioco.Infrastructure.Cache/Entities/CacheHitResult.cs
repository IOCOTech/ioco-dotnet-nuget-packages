using Ioco.Infrastructure.Cache.Enums;

namespace Ioco.Infrastructure.Cache.Entities;

/// <summary>
/// Item was found in cache and it's value was not null
/// </summary>
/// <typeparam name="T"></typeparam>
public class CacheHitResult<T> : CacheResult<T>
{
    public CacheHitResult(T data)
    {
        Data = data;
    }

    public override CacheStatus CacheStatus => CacheStatus.Hit;
}
