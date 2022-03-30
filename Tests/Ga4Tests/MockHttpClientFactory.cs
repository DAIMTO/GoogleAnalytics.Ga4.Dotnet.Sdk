using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.Text.Json;

namespace IntigrationTests;

public class MockHttpClientFactory : IHttpClientFactory
{
    private readonly string _baseAddress = "https://www.google-analytics.com";
    public HttpClient CreateClient(string name)
    {
        return new HttpClient()
        {
            BaseAddress = new Uri(_baseAddress),
            DefaultRequestHeaders =
            {
                { "Accept", "application/json" },
                { "User-Agent", "KMD Nexus Tests" }
            }
        };
    }
}