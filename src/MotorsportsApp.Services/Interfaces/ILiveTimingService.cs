using MotorsportsApp.Models.Domain;

namespace MotorsportsApp.Services.Interfaces;

public interface ILiveTimingService
{
    Task ConnectAsync(string sessionId);
    Task DisconnectAsync();
    bool IsConnected { get; }
    
    event EventHandler<Timing>? TimingDataReceived;
    event EventHandler<Weather>? WeatherDataReceived;
    event EventHandler<string>? SessionStatusChanged;
}
