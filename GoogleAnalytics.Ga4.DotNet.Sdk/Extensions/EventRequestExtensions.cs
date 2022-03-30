using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Request;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Response;

namespace GoogleAnalytics.Ga4.DotNet.Sdk.Extensions;

public static class EventRequestExtensions
{
    public static void AddEvent(this IEventRequest request, IEvent ga4Event)
    {
        if (request.Events == null)
        {
            request.Events = new List<IEvent>();
            request.Events.Add(ga4Event);
            return;
        }

        for (var i = 0; i < 20; i++)
        {
            if (string.IsNullOrWhiteSpace(request.Events[i].Name))
            {
                request.Events[i] = ga4Event;
            }
        }
        
    }

    public static async Task<EventResponse> Execute(this IEventRequest requestService, bool debug = false)
    {
        return await requestService.Send(debug);
    }
}