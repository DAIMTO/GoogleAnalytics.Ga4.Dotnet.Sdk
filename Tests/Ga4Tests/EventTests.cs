using System.Collections.Generic;
using GoogleAnalytics.Ga4.DotNet.Sdk.Extensions;
using GoogleAnalytics.Ga4.DotNet.Sdk.StandardEvents;
using Shouldly;
using Xunit;

namespace Ga4Tests;

public class EventTests 
{
    [Fact]
    public void Create_CustomEvent_With_Name_Should_Set_Be_Set()
    {
        var eventName = "TestEvent";
        
        var gaEvent = EventBuilders.BuildCustomEvent(eventName, new Dictionary<string, object>());
        
        // act
        gaEvent.ShouldNotBeNull();
        gaEvent.Name.ShouldBe(eventName);
        gaEvent.EventParameters.ShouldBeEmpty();
    }
    
    [Fact]
    public void Create_EventRequest_With_Single_String_Parameter_Should_Be_Set()
    {
        var eventName = "TestEvent";
        
        var gaEvent = EventBuilders.BuildCustomEvent(eventName, new Dictionary<string, object>()).AddParameters("test","test");
        
        // act
        gaEvent.ShouldNotBeNull();
        gaEvent.Name.ShouldBe(eventName);
        gaEvent.EventParameters.ShouldHaveSingleItem();
        gaEvent.EventParameters.ShouldContain(new KeyValuePair<string, object>("test","test"));
    }
    
    [Fact]
    public void Create_EventRequest_With_Single_Number_Parameter_Should_Be_Set()
    {
        var eventName = "TestEvent";
        
        var gaEvent = EventBuilders.BuildCustomEvent(eventName, new Dictionary<string, object>()).AddParameters("test",1);
        
        // act
        gaEvent.ShouldNotBeNull();
        gaEvent.Name.ShouldBe(eventName);
        gaEvent.EventParameters.ShouldHaveSingleItem();
        gaEvent.EventParameters.ShouldContain(new KeyValuePair<string, object>("test",1));
    }
    
    [Fact]
    public void Create_EventRequest_With_Two_Parameters_Should_Be_Set()
    {
        var eventName = "TestEvent";
        
        var gaEvent = EventBuilders.BuildCustomEvent(eventName, new Dictionary<string, object>()).
            AddParameters("test","test").
            AddParameters("test2",2);
        
        // act
        gaEvent.ShouldNotBeNull();
        gaEvent.Name.ShouldBe(eventName);
        gaEvent.EventParameters.Count.ShouldBe(2);
        gaEvent.EventParameters.ShouldContain(new KeyValuePair<string, object>("test","test"));
        gaEvent.EventParameters.ShouldContain(new KeyValuePair<string, object>("test2",2));
    }
    
    [Fact]
    public void Create_EventRequest_With_Single_Item_Should_Be_Set()
    {
        var eventName = "TestEvent";
        
        var gaEvent = EventBuilders.BuildCustomEvent(eventName, new Dictionary<string, object>())
            .AddParameters("test","test")
            .AddItems("Item1", "1");
        
        // act
        gaEvent.ShouldNotBeNull();
        gaEvent.Name.ShouldBe(eventName);
        gaEvent.EventParameters.Count.ShouldBe(2);  // one for the parameter and one for items.
        gaEvent.EventParameters.ShouldContain(new KeyValuePair<string, object>("test","test"));
        
        gaEvent.EventParameters.ShouldContainKey("items");
        gaEvent.EventParameters["items"].ShouldNotBeNull();

        var items = gaEvent.GetItems();
        items.ShouldNotBeNull();
        items.ShouldHaveSingleItem();
        items.ShouldContain(new KeyValuePair<string, object>("Item1","1"));
    }

    [Fact]
    public void Create_EventRequest_With_Single_Number_Item_Should_Be_Set()
    {
        var eventName = "TestEvent";
        
        var gaEvent = EventBuilders.BuildCustomEvent(eventName, new Dictionary<string, object>())
            .AddParameters("test","test")
            .AddItems("Item1", 1);
        
        // act
        gaEvent.ShouldNotBeNull();
        gaEvent.Name.ShouldBe(eventName);
        gaEvent.EventParameters.Count.ShouldBe(2);  // one for the parameter and one for items.
        gaEvent.EventParameters.ShouldContain(new KeyValuePair<string, object>("test","test"));
        
        gaEvent.EventParameters.ShouldContainKey("items");
        gaEvent.EventParameters["items"].ShouldNotBeNull();

        var items = gaEvent.GetItems();
        items.ShouldNotBeNull();
        items.ShouldHaveSingleItem();
        items.ShouldContain(new KeyValuePair<string, object>("Item1",1));

    }
    
    [Fact]
    public void Create_EventRequest_With_Multiple_Items_Should_Be_Set()
    {
        var eventName = "TestEvent";
        
        var gaEvent = EventBuilders.BuildCustomEvent(eventName, new Dictionary<string, object>())
            .AddParameters("test","test")
            .AddItems("Item1", "1")
            .AddItems("Item12", 1);
        
        // act
        gaEvent.ShouldNotBeNull();
        gaEvent.Name.ShouldBe(eventName);
        gaEvent.EventParameters.Count.ShouldBe(2);  // one for the parameter and one for items.
        gaEvent.EventParameters.ShouldContain(new KeyValuePair<string, object>("test","test"));
        
        gaEvent.EventParameters.ShouldContainKey("items");
        gaEvent.EventParameters["items"].ShouldNotBeNull();

        var items = gaEvent.GetItems();
        items.ShouldNotBeNull();
        items.Count.ShouldBe(2);
        items.ShouldContain(new KeyValuePair<string, object>("Item1","1"));
        items.ShouldContain(new KeyValuePair<string, object>("Item12",1));

    }
}