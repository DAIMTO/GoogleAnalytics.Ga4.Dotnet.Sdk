using GoogleAnalytics.Ga4.DotNet.Sdk.Service;
using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleAnalytics.Ga4.DotNet.Sdk.Extensions;

public static class ConfigurationExtensions
{
    /// <summary>
    /// We connect to HostName and post data.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="configurationSection"></param>
    public static void ConfigureGoogleAnalyticsService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<GoogleAnalyticsClientSettings>(configuration.GetSection("Ga4Settings"));

        services.AddHttpClient("HttpClient", client =>
        {
            client.BaseAddress = new Uri("https://www.google-analytics.com");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "Google Analytics .net sdk");
        });

        //services.AddSingleton(gaSettings);
        services.AddTransient<IBasicHttpClient, BasicHttpClient>();
        services.AddTransient<IMeasurementService, MeasurementService>();
    }
}