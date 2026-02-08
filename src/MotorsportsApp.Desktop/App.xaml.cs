using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MotorsportsApp.Core.ViewModels;
using MotorsportsApp.Data.Context;
using MotorsportsApp.Services.Interfaces;
using MotorsportsApp.Services.ApiClients;

namespace MotorsportsApp.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Database
                services.AddDbContext<MotorsportsDbContext>(options =>
                    options.UseSqlite("Data Source=motorsports.db"));

                // Services
                services.AddHttpClient<IF1DataService, ErgastApiClient>();
                services.AddSingleton<ILiveTimingService, MockLiveTimingService>();

                // ViewModels
                services.AddTransient<MainViewModel>();
                services.AddTransient<LiveTimingViewModel>();

                // Windows
                services.AddSingleton<MainWindow>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        // Ensure database is created
        using (var scope = _host.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MotorsportsDbContext>();
            await dbContext.Database.EnsureCreatedAsync();
        }

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        base.OnExit(e);
    }
}

// Temporary mock implementation for live timing
internal class MockLiveTimingService : ILiveTimingService
{
    public bool IsConnected { get; private set; }
    
    public event EventHandler<MotorsportsApp.Models.Domain.Timing>? TimingDataReceived;
    public event EventHandler<MotorsportsApp.Models.Domain.Weather>? WeatherDataReceived;
    public event EventHandler<string>? SessionStatusChanged;

    public Task ConnectAsync(string sessionId)
    {
        IsConnected = true;
        return Task.CompletedTask;
    }

    public Task DisconnectAsync()
    {
        IsConnected = false;
        return Task.CompletedTask;
    }
}

