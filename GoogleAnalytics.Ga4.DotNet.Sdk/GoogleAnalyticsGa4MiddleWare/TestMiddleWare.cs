using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GoogleAnalytics.Ga4.DotNet.Sdk;

/// <summary>
/// A middleware that enables authorization capabilities.
/// </summary>
public class TestMiddleWare
{
    private const int PortNotFound = -1;

    private readonly RequestDelegate _next;

    //private readonly Lazy<int> _httpsPort;
    private readonly int _statusCode;

    private readonly string _measurementId;
    private readonly IConfiguration _config;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes <see cref="HttpsRedirectionMiddleware" />.
    /// </summary>
    /// <param name="next"></param>
    /// <param name="options"></param>
    /// <param name="config"></param>
    /// <param name="loggerFactory"></param>
    public TestMiddleWare(RequestDelegate next, IOptions<TestMiddleWareOptions> options, IConfiguration config,
        ILoggerFactory loggerFactory)

    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _config = config ?? throw new ArgumentNullException(nameof(config));

        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        var middleWareOptions = options.Value;

        _measurementId = middleWareOptions.MeasurementId;


        _logger = loggerFactory.CreateLogger<TestMiddleWare>();
    }


    /// <summary>
    /// Invokes the TestMiddleWare.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task Invoke(HttpContext context)
    {
        // if (context.Request.IsHttps)
        // {
        //     return _next(context);
        // }

        // var port = _httpsPort.Value;
        // if (port == PortNotFound)
        // {
        //     return _next(context);
        // }

        if (!SendToGoogle(context))
        {
            return _next(context);
        }

        var controllerActionDescriptor = context?
            .GetEndpoint()?
            .Metadata
            .GetMetadata<ControllerActionDescriptor>();

        var controllerName = controllerActionDescriptor.ControllerName;
        var actionName = controllerActionDescriptor.ActionName;

        var host = context.Request.Host;
        // if (port != 443)
        // {
        //     host = new HostString(host.Host, port);
        // }
        // else
        // {
        host = new HostString(host.Host);
        //}

        var request = context.Request;
        var redirectUrl = UriHelper.BuildAbsolute(
            "https",
            host,
            request.PathBase,
            request.Path,
            request.QueryString);

        context.Response.StatusCode = _statusCode;

        return Task.CompletedTask;
    }

    public bool SendToGoogle(HttpContext context)
    {
        var controllerActionDescriptor = context?
            .GetEndpoint()?
            .Metadata
            .GetMetadata<ControllerActionDescriptor>();

        return controllerActionDescriptor.ControllerTypeInfo.CustomAttributes.Any(a =>
            a.AttributeType == typeof(GoogleAnalyticsControllerAttribute));
    }
}