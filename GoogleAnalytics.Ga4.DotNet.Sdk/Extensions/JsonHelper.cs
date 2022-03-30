using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoogleAnalytics.Ga4.DotNet.Sdk.Extensions;

public static class JsonHelper
{
    public static JsonSerializerOptions GetOptions()
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        
    }
}