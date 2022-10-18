using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Ioco.Log.Telemetry;

public class UserTelemetryInitializer : ITelemetryInitializer
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
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

        if (_httpContextAccessor.HttpContext.User.Identity == null)
        {
            return;
        }

        if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            return;
        }

        if (_httpContextAccessor.HttpContext.User.Identity is not ClaimsIdentity claimsIdentity)
        {
            return;
        }

        telemetry.Context.User.AuthenticatedUserId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    }
}
