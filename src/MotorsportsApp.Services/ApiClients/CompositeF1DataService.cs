using Microsoft.Extensions.Logging;
using MotorsportsApp.Models.Domain;
using MotorsportsApp.Services.Interfaces;

namespace MotorsportsApp.Services.ApiClients;

/// <summary>
/// Composite F1 data service that uses multiple API sources with graceful fallback
/// </summary>
public class CompositeF1DataService : IF1DataService
{
    private readonly IEnumerable<IF1DataService> _apiClients;
    private readonly ILogger<CompositeF1DataService> _logger;

    public CompositeF1DataService(
        IEnumerable<IF1DataService> apiClients,
        ILogger<CompositeF1DataService> logger)
    {
        _apiClients = apiClients;
        _logger = logger;
    }

    public async Task<IEnumerable<Session>> GetCurrentSeasonRacesAsync()
    {
        return await TryAllClientsAsync(
            client => client.GetCurrentSeasonRacesAsync(),
            "GetCurrentSeasonRaces");
    }

    public async Task<Session?> GetNextRaceAsync()
    {
        return await TryAllClientsAsync(
            client => client.GetNextRaceAsync(),
            "GetNextRace");
    }

    public async Task<IEnumerable<Driver>> GetDriversAsync(int season)
    {
        return await TryAllClientsAsync(
            client => client.GetDriversAsync(season),
            "GetDrivers");
    }

    public async Task<IEnumerable<Constructor>> GetConstructorsAsync(int season)
    {
        return await TryAllClientsAsync(
            client => client.GetConstructorsAsync(season),
            "GetConstructors");
    }

    public async Task<Session?> GetSessionAsync(int season, int round)
    {
        return await TryAllClientsAsync(
            client => client.GetSessionAsync(season, round),
            "GetSession");
    }

    private async Task<T> TryAllClientsAsync<T>(
        Func<IF1DataService, Task<T>> operation,
        string operationName)
    {
        var errors = new List<Exception>();

        foreach (var client in _apiClients)
        {
            try
            {
                _logger.LogDebug("Trying {ClientType} for {Operation}",
                    client.GetType().Name, operationName);

                var result = await operation(client);

                // Check if result is meaningful (not empty for collections)
                if (result != null && (!IsEnumerable(result) || HasItems(result)))
                {
                    _logger.LogInformation("Successfully fetched data from {ClientType} for {Operation}",
                        client.GetType().Name, operationName);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to fetch data from {ClientType} for {Operation}",
                    client.GetType().Name, operationName);
                errors.Add(ex);
            }
        }

        // All clients failed
        _logger.LogError("All API clients failed for {Operation}. Errors: {Errors}",
            operationName, string.Join(", ", errors.Select(e => e.Message)));

        return default!;
    }

    private static bool IsEnumerable<T>(T obj)
    {
        return obj is System.Collections.IEnumerable && obj is not string;
    }

    private static bool HasItems<T>(T obj)
    {
        if (obj is System.Collections.IEnumerable enumerable && obj is not string)
        {
            return enumerable.Cast<object>().Any();
        }
        return true;
    }
}
