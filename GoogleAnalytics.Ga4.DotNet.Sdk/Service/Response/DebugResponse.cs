using System.Text.Json.Serialization;

namespace GoogleAnalytics.Ga4.DotNet.Sdk.Service.Request;

public class DebugResponse
{
    [JsonPropertyName("validationMessages")]
    public List<ValidationMessage> ValidationMessages { get; set; }
}