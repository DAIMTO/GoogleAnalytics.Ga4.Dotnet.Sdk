using GoogleAnalytics.Ga4.DotNet.Sdk.Service;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Request;

namespace GoogleAnalytics.Ga4.DotNet.Sdk.Extensions;

public static class MeasurementServiceExtensions
{
    public static IEventRequest CreateEventRequest(this IMeasurementService requestService, IEventRequest request)
    {
        request.SetClient(requestService.GetClient());
        return request;
    }
}