using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Client;

namespace GoogleAnalytics.Ga4.DotNet.Sdk.Service;

public class MeasurementService : IMeasurementService
{
    private readonly IBasicHttpClient _client;
    public IBasicHttpClient GetClient()
    {
        return _client;
    }
    public MeasurementService(IBasicHttpClient client)
    {
        _client = client;
    }
}