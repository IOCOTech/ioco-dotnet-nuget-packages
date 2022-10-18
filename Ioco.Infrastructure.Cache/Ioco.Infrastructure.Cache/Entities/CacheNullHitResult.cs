using Ioco.Infrastructure.Cache.Enums;

namespace Ioco.Infrastructure.Cache.Entities;

/// <summary>
/// An item was found in cache but its value was null
/// </summary>
/// <typeparam name="T"></typeparam>
public class CacheNullHitResult<T> : CacheResult<T>
{
    public override CacheStatus CacheStatus => CacheStatus.NullHit;
}