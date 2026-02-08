using MotorsportsApp.Models.Enums;

namespace MotorsportsApp.Models.Domain;

public class Timing
{
    public string TimingId { get; set; } = string.Empty;
    public string SessionId { get; set; } = string.Empty;
    public string DriverId { get; set; } = string.Empty;
    public int Position { get; set; }
    public string? LapTime { get; set; }
    public int? Laps { get; set; }
    public TyreCompound CurrentTyre { get; set; }
    public int? TyreAge { get; set; }
    public string? Gap { get; set; }
    public string? Interval { get; set; }
    public string? Sector1 { get; set; }
    public string? Sector2 { get; set; }
    public string? Sector3 { get; set; }
    public double? Speed { get; set; }
    public bool InPit { get; set; }
    public bool DrsEnabled { get; set; }
    public DateTime Timestamp { get; set; }
}
