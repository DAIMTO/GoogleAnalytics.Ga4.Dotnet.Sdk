using System;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Request;
using Shouldly;
using Xunit;

namespace Ga4Tests;

public class EventRequestCreationTests
{
  
    [Fact]
    public  void Create_EventRequest_EmptyEverything()
    {
        
        var exception = Should.Throw<ArgumentNullException>(() => new EventRequest(null));
    }
    
    [Fact]
    public async void Create_EventRequest_ClientId_Should_Set_ClientId()
    {
        var clientId = "TestClient";
        
        var request = new EventRequest(clientId);
        
        // act
        request.ShouldNotBeNull();
        request.NonPersonalizedAds.ShouldBeFalse();
        request.ClientId.ShouldNotBeNull();
        request.ClientId.ShouldBe(clientId);
        request.UserId.ShouldBeNullOrWhiteSpace();
        request.TimestampMicros.ShouldNotBe(0);   // better way to check for min value.
    }
    
    [Fact]
    public async void Create_EventRequest_Userid_Should_Set_UserId()
    {
        var clientId = "TestClient";
        var userId = "TestUserId";
        
        var request = new EventRequest(clientId, userId);
        
        // act
        request.ShouldNotBeNull();
        request.NonPersonalizedAds.ShouldBeFalse();
        request.ClientId.ShouldNotBeNull();
        request.ClientId.ShouldBe(clientId);
        request.UserId.ShouldNotBeNullOrWhiteSpace();
        request.UserId.ShouldBe(userId);
        request.TimestampMicros.ShouldNotBe(0);   // better way to check for min value.
    }
    
    
    
    [Fact]
    public async void Create_EventRequest_NonPersonalizedAds_Should_Set_True()
    {
        var clientId = "TestClient";
        var userId = "TestUserId";
        var userProperties = 1;
        
        var request = new EventRequest(clientId, userId, true);
        
        // act
        request.ShouldNotBeNull();
        request.NonPersonalizedAds.ShouldBeTrue();
        request.ClientId.ShouldNotBeNull();
        request.ClientId.ShouldBe(clientId);
        request.UserId.ShouldNotBeNullOrWhiteSpace();
        request.UserId.ShouldBe(userId);
        
      
        
        request.TimestampMicros.ShouldNotBe(0);   // better way to check for min value.
    }
}