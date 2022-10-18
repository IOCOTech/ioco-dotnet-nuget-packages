using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;

namespace Ioco.Log.Telemetry;

public class IPTelemetryInitializer : ITelemetryInitializer
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public IPTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Initialize(ITelemetry telemetry)
    {
        if (_httpContextAccessor == null)
        {
            return;
        }

        if (_httpContextAccessor.HttpContext == null)
        {
            return;
        }

        ISupportProperties propTelemetry = (ISupportProperties)telemetry;

        if (propTelemetry.Properties.ContainsKey("Client IP"))
        {
            return; // hope in faith that the existing value is correct
        }

        // cloud flare requests are sent from their servers
        // but they are nice to foward us the client ips on a request header
        var clientIp = _httpContextAccessor.HttpContext.Request.Headers["CF-Connecting-IP"].FirstOrDefault();

        if (string.IsNullOrEmpty(clientIp))
        {
            clientIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        if (string.IsNullOrEmpty(clientIp))
        {
            return;
        }

        propTelemetry.Properties.Add("Client IP", clientIp);
    }
}
