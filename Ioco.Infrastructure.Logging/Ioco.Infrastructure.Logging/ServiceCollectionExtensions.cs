using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ioco.Log.Telemetry;

namespace Ioco.Infrastructure.Logging;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddLoggingSupport(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddApplicationInsightsTelemetry();

        services.Configure<LoggingSettings>(configuration.GetSection(LoggingSettings.ConfigSectionName));

        services.AddSingleton<ITelemetryInitializer, CloudRoleNameInitializer>();
        services.AddSingleton<ITelemetryInitializer, UserTelemetryInitializer>();
        services.AddSingleton<ITelemetryInitializer, IPTelemetryInitializer>();
        services.AddSingleton<ITelemetryInitializer, UrlTelemetryInitializer>();

        return services;

    }
}
