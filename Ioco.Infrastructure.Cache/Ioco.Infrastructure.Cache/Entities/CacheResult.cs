using Ioco.Infrastructure.Cache.Enums;
using Ioco.Infrastructure.Cache.Interfaces;

namespace Ioco.Infrastructure.Cache.Entities;

public abstract class CacheResult<T> : ICacheResult<T>
{
    public T? Data { get; set; }

    public abstract CacheStatus CacheStatus { get; }
}
