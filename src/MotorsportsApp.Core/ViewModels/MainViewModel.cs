using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MotorsportsApp.Models.Domain;
using MotorsportsApp.Services.Interfaces;
using System.Collections.ObjectModel;

namespace MotorsportsApp.Core.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IF1DataService _dataService;

    [ObservableProperty]
    private string _title = "F1 Live Timing";

    [ObservableProperty]
    private Session? _currentSession;

    [ObservableProperty]
    private bool _isLoading;

    public ObservableCollection<Session> UpcomingRaces { get; } = new();
    public ObservableCollection<Driver> Drivers { get; } = new();

    public MainViewModel(IF1DataService dataService)
    {
        _dataService = dataService;
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        try
        {
            IsLoading = true;

            // Load upcoming races
            var races = await _dataService.GetCurrentSeasonRacesAsync();
            UpcomingRaces.Clear();
            foreach (var race in races)
            {
                UpcomingRaces.Add(race);
            }

            // Load drivers
            var currentYear = DateTime.Now.Year;
            var drivers = await _dataService.GetDriversAsync(currentYear);
            Drivers.Clear();
            foreach (var driver in drivers)
            {
                Drivers.Add(driver);
            }

            // Set current/next session
            CurrentSession = await _dataService.GetNextRaceAsync();
        }
        catch (Exception ex)
        {
            // Log error
            System.Diagnostics.Debug.WriteLine($"Error loading data: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
}
