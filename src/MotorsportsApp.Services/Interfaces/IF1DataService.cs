using MotorsportsApp.Models.Domain;

namespace MotorsportsApp.Services.Interfaces;

public interface IF1DataService
{
    Task<IEnumerable<Session>> GetCurrentSeasonRacesAsync();
    Task<Session?> GetNextRaceAsync();
    Task<IEnumerable<Driver>> GetDriversAsync(int season);
    Task<IEnumerable<Constructor>> GetConstructorsAsync(int season);
    Task<Session?> GetSessionAsync(int season, int round);
}
