using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MotorsportsApp.Models.Domain;
using MotorsportsApp.Services.Interfaces;

namespace MotorsportsApp.Services.Caching;

/// <summary>
/// Caching decorator for F1 data service to reduce API calls
/// </summary>
public class CachedF1DataService : IF1DataService
{
    private readonly IF1DataService _innerService;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachedF1DataService> _logger;

    // Cache durations
    private static readonly TimeSpan RacesCacheDuration = TimeSpan.FromHours(6);
    private static readonly TimeSpan DriversCacheDuration = TimeSpan.FromDays(1);
    private static readonly TimeSpan ConstructorsCacheDuration = TimeSpan.FromDays(1);
    private static readonly TimeSpan SessionCacheDuration = TimeSpan.FromMinutes(30);

    public CachedF1DataService(
        IF1DataService innerService,
        IMemoryCache cache,
        ILogger<CachedF1DataService> logger)
    {
        _innerService = innerService;
        _cache = cache;
        _logger = logger;
    }

    public async Task<IEnumerable<Session>> GetCurrentSeasonRacesAsync()
    {
        var cacheKey = $"races_{DateTime.Now.Year}";
        
        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            _logger.LogInformation("Cache miss for {CacheKey}, fetching from API", cacheKey);
            entry.AbsoluteExpirationRelativeToNow = RacesCacheDuration;
            return await _innerService.GetCurrentSeasonRacesAsync();
        }) ?? Enumerable.Empty<Session>();
    }

    public async Task<Session?> GetNextRaceAsync()
    {
        var cacheKey = "next_race";
        
        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            _logger.LogInformation("Cache miss for {CacheKey}, fetching from API", cacheKey);
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return await _innerService.GetNextRaceAsync();
        });
    }

    public async Task<IEnumerable<Driver>> GetDriversAsync(int season)
    {
        var cacheKey = $"drivers_{season}";
        
        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            _logger.LogInformation("Cache miss for {CacheKey}, fetching from API", cacheKey);
            entry.AbsoluteExpirationRelativeToNow = DriversCacheDuration;
            return await _innerService.GetDriversAsync(season);
        }) ?? Enumerable.Empty<Driver>();
    }

    public async Task<IEnumerable<Constructor>> GetConstructorsAsync(int season)
    {
        var cacheKey = $"constructors_{season}";
        
        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            _logger.LogInformation("Cache miss for {CacheKey}, fetching from API", cacheKey);
            entry.AbsoluteExpirationRelativeToNow = ConstructorsCacheDuration;
            return await _innerService.GetConstructorsAsync(season);
        }) ?? Enumerable.Empty<Constructor>();
    }

    public async Task<Session?> GetSessionAsync(int season, int round)
    {
        var cacheKey = $"session_{season}_{round}";
        
        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            _logger.LogInformation("Cache miss for {CacheKey}, fetching from API", cacheKey);
            entry.AbsoluteExpirationRelativeToNow = SessionCacheDuration;
            return await _innerService.GetSessionAsync(season, round);
        });
    }
}
