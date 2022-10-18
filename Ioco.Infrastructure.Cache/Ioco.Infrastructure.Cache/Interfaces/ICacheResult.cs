using Ioco.Infrastructure.Cache.Enums;

namespace Ioco.Infrastructure.Cache.Interfaces;

public interface ICacheResult<T>
{
    public T? Data { get; set; }
    public CacheStatus CacheStatus { get; }
}
