using Ioco.Infrastructure.Cache.Enums;

namespace Ioco.Infrastructure.Cache.Entities;

/// <summary>
/// No item was found in cache
/// </summary>
/// <typeparam name="T"></typeparam>
public class CacheMissResult<T> : CacheResult<T>
{
    public override CacheStatus CacheStatus => CacheStatus.Miss;
}