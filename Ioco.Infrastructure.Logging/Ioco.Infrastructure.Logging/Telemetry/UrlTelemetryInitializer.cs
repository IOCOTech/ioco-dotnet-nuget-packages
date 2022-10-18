using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Ioco.Log.Telemetry;

public class UrlTelemetryInitializer : ITelemetryInitializer
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UrlTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
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

        var isException = telemetry is ExceptionTelemetry;
        var isTrace = telemetry is TraceTelemetry;

        if (!isException && !isTrace)
        {
            return; // we only care about urls on trace and exceptions for now are they are a lot ot telemetries and we want to reduce the amount of data posted
        }

        ISupportProperties propTelemetry = (ISupportProperties)telemetry;

        if (propTelemetry.Properties.ContainsKey("Url"))
        {
            return; // hope in faith that the existing value is correct
        }

        var url = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();


        if (string.IsNullOrEmpty(url))
        {
            return;
        }

        propTelemetry.Properties.Add("Url", url);
    }
}
