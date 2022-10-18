using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Options;
using Ioco.Infrastructure.Logging;

namespace Ioco.Log.Telemetry;

public class CloudRoleNameInitializer : ITelemetryInitializer
{
    private readonly string cloudRoleName;

    public CloudRoleNameInitializer(IOptions<LoggingSettings> options)
    {
        cloudRoleName = options?.Value?.Tag ?? "not_set";
    }

    public void Initialize(ITelemetry telemetry)
    {
        telemetry.Context.Cloud.RoleName = cloudRoleName;
    }
}
