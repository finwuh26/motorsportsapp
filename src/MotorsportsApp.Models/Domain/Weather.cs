namespace MotorsportsApp.Models.Domain;

public class Weather
{
    public string WeatherId { get; set; } = string.Empty;
    public string SessionId { get; set; } = string.Empty;
    public double? AirTemperature { get; set; }
    public double? TrackTemperature { get; set; }
    public int? Humidity { get; set; }
    public double? Pressure { get; set; }
    public double? WindSpeed { get; set; }
    public int? WindDirection { get; set; }
    public int? Rainfall { get; set; }
    public DateTime Timestamp { get; set; }
}
