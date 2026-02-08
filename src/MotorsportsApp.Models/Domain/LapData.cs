namespace MotorsportsApp.Models.Domain;

public class LapData
{
    public string LapDataId { get; set; } = string.Empty;
    public string SessionId { get; set; } = string.Empty;
    public string DriverId { get; set; } = string.Empty;
    public int LapNumber { get; set; }
    public TimeSpan? LapTime { get; set; }
    public TimeSpan? Sector1Time { get; set; }
    public TimeSpan? Sector2Time { get; set; }
    public TimeSpan? Sector3Time { get; set; }
    public double? TopSpeed { get; set; }
    public int? Position { get; set; }
    public bool IsPersonalBest { get; set; }
    public bool IsOverallBest { get; set; }
}
