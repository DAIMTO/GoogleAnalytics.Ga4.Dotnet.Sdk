# GoogleAnalytics.Ga4-Dotnet.Sdk - Beta


This is an sdk designed for use with the [Google analytics ga4 measurement protocol](https://developers.google.com/analytics/devguides/collection/protocol/ga4)


# settings

    "Ga4Settings": {
       "MeasurmentId": "G-XXXXX",
       "AppSecret": "XXXX"
    }


# Configure add di

    builder.Services.ConfigureGoogleAnalyticsService(configuration);


# The measure service

Requests are sent though the MeasurementService.   

    private readonly IMeasurementService _service;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMeasurementService service)
    {
        _logger = logger;
        _service = service;
    }


# debug an event

Note this will send the request to the validation end point it will not send hits to google analytics

    var gaEvent = EventBuilders.BuildCustomEvent("test_event", new Dictionary<string, object>()).AddParameters("test","test");
    var request = new EventRequest("ClientId");
    request.AddEvent(gaEvent);


    var result = await _service.CreateEventRequest(request).Execute(true);


# Send hit

This will send a hit to google analytics the only change is in Execute();

    var gaEvent = EventBuilders.BuildCustomEvent("test_event", new Dictionary<string, object>()).AddParameters("test","test");
    var request = new EventRequest("ClientId");
    request.AddEvent(gaEvent);


    var result = await _service.CreateEventRequest(request).Execute();
