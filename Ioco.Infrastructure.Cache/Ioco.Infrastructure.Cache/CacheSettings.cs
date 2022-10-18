namespace Ioco.Infrastructure.Cache
{
    public class CacheSettings
    {
        public const string ConfigSectionName = "CacheConfig";

        public string InstanceName { get; set; } = null!;

        public int NullCacheDurationInSeconds { get; set; }

        public int DefaultCacheDurationInSeconds { get; set; }
    }
}
