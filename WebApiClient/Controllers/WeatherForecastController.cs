using System.Text.Json;
using GoogleAnalytics.Ga4.DotNet.Sdk.Extensions;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Request;
using GoogleAnalytics.Ga4.DotNet.Sdk.StandardEvents;
using Microsoft.AspNetCore.Mvc;

namespace WebApiClient.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMeasurementService _service;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMeasurementService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
    
        
        var gaEvent = EventBuilders.BuildCustomEvent("test_event", new Dictionary<string, object>()).AddParameters("test","test");
        var request = new EventRequest("ClientId");
        request.AddEvent(gaEvent);
        var result = await _service.CreateEventRequest(request).Execute(true);
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}