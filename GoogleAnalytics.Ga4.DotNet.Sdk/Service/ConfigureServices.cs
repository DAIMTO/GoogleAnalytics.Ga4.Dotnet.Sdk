using GoogleAnalytics.Ga4.DotNet.Sdk.Service.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleAnalytics.Ga4.DotNet.Sdk.Service;

public static class ConfigureServices
{
    /// <summary>
    /// We connect to HostName and post data.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="configurationSection"></param>
    public static void ConfigureService(this IServiceCollection services, IConfiguration configuration,
        string configurationSection)
    {
        // get config settings
        var options = configuration.GetSection(configurationSection).Get<HttpClientSettings>();
        //var gaSettings = configuration.GetSection("GASettings").Get<GASettings>();
        
        services.Configure<HttpClientSettings>(configuration.GetSection(configurationSection));
        services.Configure<GoogleAnalyticsClientSettings>(configuration.GetSection("GASettings"));

        services.AddHttpClient(options.Name, client =>
        {
            client.BaseAddress = new Uri(options.HostName);
            client.DefaultRequestHeaders.Add("Accept", options.ContentType);
            client.DefaultRequestHeaders.Add("User-Agent", options.UserAgent);
            
        });

        //services.AddSingleton(gaSettings);
        services.AddTransient<IBasicHttpClient, BasicHttpClient>();
        services.AddTransient<IMeasurementService, MeasurementService>();
    }
}