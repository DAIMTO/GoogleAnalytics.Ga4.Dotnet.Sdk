using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Client;

namespace GoogleAnalytics.Ga4.DotNet.Sdk.Service;

public interface IMeasurementService
{
    IBasicHttpClient GetClient();
}