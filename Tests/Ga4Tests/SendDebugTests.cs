using FakeItEasy;
using GoogleAnalytics.Ga4.DotNet.Sdk.Extensions;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Client;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Request;
using IntigrationTests;
using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace Ga4Tests;




public class SendDebugTests
{
    public static IOptions<GoogleAnalyticsClientSettings> Settings()
    {
        return Options.Create(new GoogleAnalyticsClientSettings()
        {
            AppSecret = "XXXXX",
            MeasurmentId = "G-XXXXXX",
        });
    }
    
    
    [Fact]
    public async void SendDebug_No_Event_Should_Work()
    {
        // arrange
        var clientFactory = new MockHttpClientFactory();
        var clientOptions = A.Fake<IOptions<HttpClientSettings>>();
        var gaOptions = Settings();

        var client = new BasicHttpClient(clientFactory, gaOptions);

        // act  
        var service = new MeasurementService(client);
        var result = await service.CreateEventRequest(new EventRequest("ClientId")).Execute(true);

        // act
        result.IsSuccess.ShouldBeTrue();
        result.IsRequestValid().ShouldBeTrue();
        result.Message.ShouldNotBeNull();
        result.DebugResponse.ShouldNotBeNull();
        result.DebugResponse.ValidationMessages.Count.ShouldBe(0);
    }


    [Fact]
    public async void SendDebug_An_Event_With_ClientId_Should_Work()
    {
        // arrange
        var clientFactory = new MockHttpClientFactory();
        var gaOptions =  Settings();


        var client = new BasicHttpClient(clientFactory, gaOptions);


        // act  
        var service = new MeasurementService(client);
        var result = await service.CreateEventRequest(new EventRequest("TestClient")).Execute(true);
        
        // act
        result.IsSuccess.ShouldBeTrue();
        result.IsRequestValid().ShouldBeTrue();
        result.Message.ShouldNotBeNull();
        result.DebugResponse.ShouldNotBeNull();
        result.DebugResponse.ValidationMessages.Count.ShouldBe(0);
    }
    
    [Fact]
    public async void SendDebug_An_Event_with_user_Should_Work()
    {
        // arrange
        var clientFactory = new MockHttpClientFactory();
        var gaOptions =  Settings();


        var client = new BasicHttpClient(clientFactory, gaOptions);


        // act  
        
        var clientId = "TestClient";
        var userId = "TestUserId";
        
        var request = new EventRequest(clientId, userId);
        
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
    public async void SendDebug_An_Event_with_NonPersonaAds_Should_Work()
    {
        // arrange
        var clientFactory = new MockHttpClientFactory();
        var gaOptions =  Settings();

        var client = new BasicHttpClient(clientFactory, gaOptions);

        // act  
        var clientId = "TestClient";
        var userId = "TestUserId";
        var userProperties = 1;
        
        var request = new EventRequest(clientId, userId, true);
        
        var service = new MeasurementService(client);
        var result = await service.CreateEventRequest(request).Execute(true);

        // act
        result.IsSuccess.ShouldBeTrue();
    }
}