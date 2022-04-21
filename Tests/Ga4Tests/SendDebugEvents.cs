using System.Collections.Generic;
using System.Text.Json;
using FakeItEasy;
using GoogleAnalytics.Ga4.DotNet.Sdk.Extensions;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Client;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Request;
using GoogleAnalytics.Ga4.DotNet.Sdk.StandardEvents;
using IntigrationTests;
using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace Ga4Tests;

public class SendDebugEvents
{
    [Fact]
    public async void SendDebug_SingleEvent_Should_Work()
    {
        // arrange
        var clientFactory = new MockHttpClientFactory();
        var gaOptions = SendDebugTests.Settings();

        var client = new BasicHttpClient(clientFactory, gaOptions);

        var eventName = "TestEvent";

        var gaEvent = EventBuilders.BuildCustomEvent(eventName, new Dictionary<string, object>()).AddParameters("test","test");
        var request = new EventRequest("ClientId");
        request.AddEvent(gaEvent);

        var hold =JsonSerializer.Serialize(request);
        
        // act  
        var service = new MeasurementService(client);
        var result = await service.CreateEventRequest(request).Execute(true);

        // act
        result.IsSuccess.ShouldBeTrue();
        result.IsRequestValid().ShouldBeTrue();
        result.Message.ShouldNotBeNull();
        result.DebugResponse.ShouldNotBeNull();
        result.DebugResponse.ValidationMessages.Count.ShouldBe(0);
    }
    
    [Fact]
    public async void SendDebug_SingleEvent_With_UserProperties_Should_Work()
    {
        // arrange
        var clientFactory = new MockHttpClientFactory();
        var gaOptions = SendDebugTests.Settings();

        var client = new BasicHttpClient(clientFactory, gaOptions);

        var eventName = "TestEvent";

        var gaEvent = EventBuilders.BuildCustomEvent(eventName, new Dictionary<string, object>()).AddParameters("test","test");
        var request = new EventRequest("ClientId");
        request.AddEvent(gaEvent);
        
        request.UserProperties = new Dictionary<string, object>()
        {
            { "favorite_food", "apples" },
            { "favorite_color", "green" },
        };

        var hold =JsonSerializer.Serialize(request);
        
        // act  
        var service = new MeasurementService(client);
        var result = await service.CreateEventRequest(request).Execute(true);

        // act
        result.IsSuccess.ShouldBeTrue();
        result.IsRequestValid().ShouldBeTrue();
        result.Message.ShouldNotBeNull();
        result.DebugResponse.ShouldNotBeNull();
        result.DebugResponse.ValidationMessages.Count.ShouldBe(0);
    }

    
    [Fact]
    public async void SendDebug_SingleEvent_With_One_Item_Should_Work()
    {
        // arrange
        var clientFactory = new MockHttpClientFactory();
        var gaOptions = SendDebugTests.Settings();

        var client = new BasicHttpClient(clientFactory, gaOptions);

        var eventName = "TestEvent";

        var gaEvent = EventBuilders.BuildCustomEvent(eventName, new Dictionary<string, object>())
            .AddParameters("test","test")
            .AddItems("Item1", 1);
        var request = new EventRequest("ClientId");
        request.AddEvent(gaEvent);

        var hold =JsonSerializer.Serialize(request);
        
        // act  
        var service = new MeasurementService(client);
        var result = await service.CreateEventRequest(request).Execute(true);

        // act
        result.IsSuccess.ShouldBeTrue();
        result.IsRequestValid().ShouldBeTrue();
        result.Message.ShouldNotBeNull();
        result.DebugResponse.ShouldNotBeNull();
        result.DebugResponse.ValidationMessages.Count.ShouldBe(0);
    }
    
    [Fact]
    public async void SendDebug_SingleEvent_With_Two_Items_Should_Work()
    {
        // arrange
        var clientFactory = new MockHttpClientFactory();
        var gaOptions = SendDebugTests.Settings();

        var client = new BasicHttpClient(clientFactory, gaOptions);

        var eventName = "TestEvent";

        var gaEvent = EventBuilders.BuildCustomEvent(eventName, new Dictionary<string, object>()).
            AddParameters("test","test").
            AddItems("Item1", 1).
            AddItems("Item12", "1");
        var request = new EventRequest("ClientId");
        request.AddEvent(gaEvent);

        var hold =JsonSerializer.Serialize(request);
        
        // act  
        var service = new MeasurementService(client);
        var result = await service.CreateEventRequest(request).Execute(true);

        // act
        result.IsSuccess.ShouldBeTrue();
        result.IsRequestValid().ShouldBeTrue();
        result.Message.ShouldNotBeNull();
        result.DebugResponse.ShouldNotBeNull();
        result.DebugResponse.ValidationMessages.Count.ShouldBe(0);
    }
}