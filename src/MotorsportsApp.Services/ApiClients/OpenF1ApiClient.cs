using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using MotorsportsApp.Models.Domain;
using MotorsportsApp.Services.Interfaces;

namespace MotorsportsApp.Services.ApiClients;

public class OpenF1ApiClient : IF1DataService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OpenF1ApiClient> _logger;
    private const string BaseUrl = "https://api.openf1.org/v1";

    public OpenF1ApiClient(HttpClient httpClient, ILogger<OpenF1ApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<IEnumerable<Session>> GetCurrentSeasonRacesAsync()
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var response = await _httpClient.GetFromJsonAsync<List<SessionDto>>(
                $"/sessions?year={currentYear}");
            
            if (response == null)
                return Array.Empty<Session>();

            return response
                .Where(s => s.SessionType?.ToLower() == "race")
                .Select(s => new Session
                {
                    SessionId = $"{s.SessionKey}",
                    Season = currentYear,
                    Round = s.RoundNumber ?? 0,
                    SessionType = Models.Enums.SessionType.Race,
                    RaceName = s.SessionName ?? "Unknown",
                    SessionDate = s.DateStart,
                    IsLive = s.DateEnd == null || s.DateEnd > DateTime.UtcNow
                })
                .OrderBy(s => s.Round);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching sessions from OpenF1 API");
            return Array.Empty<Session>();
        }
    }

    public async Task<Session?> GetNextRaceAsync()
    {
        var races = await GetCurrentSeasonRacesAsync();
        return races.FirstOrDefault(r => r.SessionDate >= DateTime.UtcNow);
    }

    public async Task<IEnumerable<Driver>> GetDriversAsync(int season)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<DriverDto>>(
                $"/drivers?session_key=latest");
            
            if (response == null)
                return Array.Empty<Driver>();

            return response.Select(d => new Driver
            {
                DriverId = d.DriverNumber?.ToString() ?? "0",
                Code = d.NameAcronym ?? string.Empty,
                PermanentNumber = d.DriverNumber?.ToString() ?? string.Empty,
                GivenName = d.FirstName ?? string.Empty,
                FamilyName = d.LastName ?? string.Empty,
                TeamId = d.TeamName ?? string.Empty
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching drivers from OpenF1 API");
            return Array.Empty<Driver>();
        }
    }

    public Task<IEnumerable<Constructor>> GetConstructorsAsync(int season)
    {
        // OpenF1 doesn't have a dedicated constructors endpoint
        // Return empty for now, fallback to other APIs
        return Task.FromResult(Enumerable.Empty<Constructor>());
    }

    public async Task<Session?> GetSessionAsync(int season, int round)
    {
        var races = await GetCurrentSeasonRacesAsync();
        return races.FirstOrDefault(r => r.Season == season && r.Round == round);
    }

    // DTOs for OpenF1 API
    private class SessionDto
    {
        public int? SessionKey { get; set; }
        public string? SessionName { get; set; }
        public string? SessionType { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? RoundNumber { get; set; }
        public string? CircuitShortName { get; set; }
        public string? CountryName { get; set; }
    }

    private class DriverDto
    {
        public int? DriverNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NameAcronym { get; set; }
        public string? TeamName { get; set; }
        public string? TeamColour { get; set; }
    }
}
