namespace MotorsportsApp.Services.Interfaces;

public interface IApiHealthCheckService
{
    Task<ApiHealthStatus> CheckHealthAsync();
    Task<Dictionary<string, ApiEndpointHealth>> CheckAllEndpointsAsync();
}

public class ApiHealthStatus
{
    public bool IsHealthy { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CheckedAt { get; set; } = DateTime.UtcNow;
}

public class ApiEndpointHealth
{
    public string Name { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public TimeSpan ResponseTime { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime CheckedAt { get; set; } = DateTime.UtcNow;
}
