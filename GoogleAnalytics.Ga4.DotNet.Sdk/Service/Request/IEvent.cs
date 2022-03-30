using System.Text.Json.Serialization;

namespace GoogleAnalytics.Ga4.DotNet.Sdk.Service.Request;

public interface IEvent
{ 
    [JsonPropertyName("name")] 
    string Name { get; set; }
    [JsonPropertyName("params")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    Dictionary<string, object>  EventParameters { get; set; }
}