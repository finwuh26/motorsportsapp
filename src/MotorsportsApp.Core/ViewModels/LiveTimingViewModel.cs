using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MotorsportsApp.Models.Domain;
using MotorsportsApp.Services.Interfaces;
using System.Collections.ObjectModel;

namespace MotorsportsApp.Core.ViewModels;

public partial class LiveTimingViewModel : ObservableObject
{
    private readonly ILiveTimingService _liveTimingService;
    private readonly IF1DataService _dataService;

    [ObservableProperty]
    private Session? _currentSession;

    [ObservableProperty]
    private bool _isConnected;

    [ObservableProperty]
    private Weather? _currentWeather;

    public ObservableCollection<Timing> LeaderBoard { get; } = new();

    public LiveTimingViewModel(ILiveTimingService liveTimingService, IF1DataService dataService)
    {
        _liveTimingService = liveTimingService;
        _dataService = dataService;

        _liveTimingService.TimingDataReceived += OnTimingDataReceived;
        _liveTimingService.WeatherDataReceived += OnWeatherDataReceived;
    }

    [RelayCommand]
    private async Task ConnectToSessionAsync(string sessionId)
    {
        try
        {
            await _liveTimingService.ConnectAsync(sessionId);
            IsConnected = _liveTimingService.IsConnected;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error connecting to session: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task DisconnectAsync()
    {
        await _liveTimingService.DisconnectAsync();
        IsConnected = false;
    }

    private void OnTimingDataReceived(object? sender, Timing timing)
    {
        // Update leaderboard
        var existing = LeaderBoard.FirstOrDefault(t => t.DriverId == timing.DriverId);
        if (existing != null)
        {
            LeaderBoard.Remove(existing);
        }
        LeaderBoard.Add(timing);

        // Sort by position
        var sorted = LeaderBoard.OrderBy(t => t.Position).ToList();
        LeaderBoard.Clear();
        foreach (var item in sorted)
        {
            LeaderBoard.Add(item);
        }
    }

    private void OnWeatherDataReceived(object? sender, Weather weather)
    {
        CurrentWeather = weather;
    }
}
