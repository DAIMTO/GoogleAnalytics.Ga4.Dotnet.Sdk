using System.Net.Http.Json;
using System.Text.Json;
using GoogleAnalytics.Ga4.DotNet.Sdk.Extensions;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Request;
using Microsoft.Extensions.Options;

namespace GoogleAnalytics.Ga4.DotNet.Sdk.Service.Client;

public class BasicHttpClient: IBasicHttpClient
{
    private readonly GoogleAnalyticsClientSettings _settings;
    private readonly HttpClient _client;
    

    public BasicHttpClient(IHttpClientFactory httpClientFactory, IOptions<GoogleAnalyticsClientSettings> settings)
    {
        _settings = settings.Value;
        _client = httpClientFactory.CreateClient("HttpClient");
    }
    
    public async Task<HttpResponseMessage> PostAsync(string path, IEventRequest data)
    {
        var fullPath = $"{path}?measurement_id={_settings.MeasurementId}&api_secret=${_settings.AppSecret}";
        var hold = JsonSerializer.Serialize(data);
        
        // Send Message to GateWay
        return await _client.PostAsJsonAsync(fullPath, data, JsonHelper.GetOptions());
    }
}