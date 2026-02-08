using System.Diagnostics;
using Microsoft.Extensions.Logging;
using MotorsportsApp.Services.Interfaces;

namespace MotorsportsApp.Services.ApiClients;

public class ApiHealthCheckService : IApiHealthCheckService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiHealthCheckService> _logger;

    private readonly Dictionary<string, string> _endpoints = new()
    {
        { "Ergast", "https://ergast.com/api/f1/current.json" },
        { "OpenF1", "https://api.openf1.org/v1/sessions?limit=1" }
    };

    public ApiHealthCheckService(HttpClient httpClient, ILogger<ApiHealthCheckService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpClient.Timeout = TimeSpan.FromSeconds(5);
    }

    public async Task<ApiHealthStatus> CheckHealthAsync()
    {
        var results = await CheckAllEndpointsAsync();
        var healthyCount = results.Values.Count(h => h.IsAvailable);
        var totalCount = results.Count;

        return new ApiHealthStatus
        {
            IsHealthy = healthyCount > 0,
            Message = $"{healthyCount}/{totalCount} API endpoints available",
            CheckedAt = DateTime.UtcNow
        };
    }

    public async Task<Dictionary<string, ApiEndpointHealth>> CheckAllEndpointsAsync()
    {
        var results = new Dictionary<string, ApiEndpointHealth>();

        foreach (var endpoint in _endpoints)
        {
            var health = await CheckEndpointAsync(endpoint.Key, endpoint.Value);
            results[endpoint.Key] = health;
        }

        return results;
    }

    private async Task<ApiEndpointHealth> CheckEndpointAsync(string name, string url)
    {
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            var response = await _httpClient.GetAsync(url);
            stopwatch.Stop();

            return new ApiEndpointHealth
            {
                Name = name,
                IsAvailable = response.IsSuccessStatusCode,
                ResponseTime = stopwatch.Elapsed,
                ErrorMessage = response.IsSuccessStatusCode ? null : $"HTTP {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogWarning(ex, "Health check failed for {EndpointName}", name);

            return new ApiEndpointHealth
            {
                Name = name,
                IsAvailable = false,
                ResponseTime = stopwatch.Elapsed,
                ErrorMessage = ex.Message
            };
        }
    }
}
