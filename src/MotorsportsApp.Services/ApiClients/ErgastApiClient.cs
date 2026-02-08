using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using MotorsportsApp.Models.Domain;
using MotorsportsApp.Models.Enums;
using MotorsportsApp.Services.Interfaces;

namespace MotorsportsApp.Services.ApiClients;

public class ErgastApiClient : IF1DataService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ErgastApiClient> _logger;
    private const string BaseUrl = "https://ergast.com/api/f1";

    public ErgastApiClient(HttpClient httpClient, ILogger<ErgastApiClient> logger)
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
            var response = await _httpClient.GetFromJsonAsync<ErgastResponse<RaceTable>>(
                $"/{currentYear}.json");
            
            if (response?.MRData?.RaceTable?.Races == null)
                return Array.Empty<Session>();

            return response.MRData.RaceTable.Races.Select((r, index) => new Session
            {
                SessionId = $"{currentYear}-{r.Round}",
                Season = currentYear,
                Round = r.Round,
                SessionType = SessionType.Race,
                RaceName = r.RaceName,
                Circuit = new Circuit
                {
                    CircuitId = r.Circuit.CircuitId,
                    CircuitName = r.Circuit.CircuitName,
                    Locality = r.Circuit.Location.Locality,
                    Country = r.Circuit.Location.Country,
                    Latitude = r.Circuit.Location.Lat,
                    Longitude = r.Circuit.Location.Long
                },
                SessionDate = DateTime.TryParse(r.Date, out var date) ? date : null
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching races from Ergast API");
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
            var response = await _httpClient.GetFromJsonAsync<ErgastResponse<DriverTable>>(
                $"/{season}/drivers.json");
            
            if (response?.MRData?.DriverTable?.Drivers == null)
                return Array.Empty<Driver>();

            return response.MRData.DriverTable.Drivers.Select(d => new Driver
            {
                DriverId = d.DriverId,
                Code = d.Code ?? string.Empty,
                PermanentNumber = d.PermanentNumber ?? string.Empty,
                GivenName = d.GivenName,
                FamilyName = d.FamilyName,
                DateOfBirth = DateTime.TryParse(d.DateOfBirth, out var dob) ? dob : null,
                Nationality = d.Nationality
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching drivers from Ergast API");
            return Array.Empty<Driver>();
        }
    }

    public async Task<IEnumerable<Constructor>> GetConstructorsAsync(int season)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ErgastResponse<ConstructorTable>>(
                $"/{season}/constructors.json");
            
            if (response?.MRData?.ConstructorTable?.Constructors == null)
                return Array.Empty<Constructor>();

            return response.MRData.ConstructorTable.Constructors.Select(c => new Constructor
            {
                ConstructorId = c.ConstructorId,
                Name = c.Name,
                Nationality = c.Nationality,
                Url = c.Url
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching constructors from Ergast API");
            return Array.Empty<Constructor>();
        }
    }

    public async Task<Session?> GetSessionAsync(int season, int round)
    {
        var races = await GetCurrentSeasonRacesAsync();
        return races.FirstOrDefault(r => r.Season == season && r.Round == round);
    }

    // DTOs for Ergast API
    private class ErgastResponse<T>
    {
        public MRData<T>? MRData { get; set; }
    }

    private class MRData<T>
    {
        public T? RaceTable { get; set; }
        public T? DriverTable { get; set; }
        public T? ConstructorTable { get; set; }
    }

    private class RaceTable
    {
        public List<Race>? Races { get; set; }
    }

    private class Race
    {
        public int Round { get; set; }
        public string RaceName { get; set; } = string.Empty;
        public CircuitDto Circuit { get; set; } = new();
        public string? Date { get; set; }
        public string? Time { get; set; }
    }

    private class CircuitDto
    {
        public string CircuitId { get; set; } = string.Empty;
        public string CircuitName { get; set; } = string.Empty;
        public LocationDto Location { get; set; } = new();
    }

    private class LocationDto
    {
        public string Locality { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public double? Lat { get; set; }
        public double? Long { get; set; }
    }

    private class DriverTable
    {
        public List<DriverDto>? Drivers { get; set; }
    }

    private class DriverDto
    {
        public string DriverId { get; set; } = string.Empty;
        public string? Code { get; set; }
        public string? PermanentNumber { get; set; }
        public string GivenName { get; set; } = string.Empty;
        public string FamilyName { get; set; } = string.Empty;
        public string? DateOfBirth { get; set; }
        public string Nationality { get; set; } = string.Empty;
    }

    private class ConstructorTable
    {
        public List<ConstructorDto>? Constructors { get; set; }
    }

    private class ConstructorDto
    {
        public string ConstructorId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
