using System.Text.Json.Serialization;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Client;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Response;

namespace GoogleAnalytics.Ga4.DotNet.Sdk.Service.Request;

public interface  IEventRequest
{
    [JsonPropertyName("client_id")] 
    public string ClientId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("user_id")] 
    public string UserId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("timestamp_micros")] 
    public int TimestampMicros { get; set; }

    [JsonPropertyName("non_personalized_ads")]
    public bool NonPersonalizedAds { get; set; }

    [JsonPropertyName("events")] 
    public List<IEvent> Events { get; set; }

    Task<EventResponse> Send(bool debug = false);

    void SetClient(IBasicHttpClient client);


}

