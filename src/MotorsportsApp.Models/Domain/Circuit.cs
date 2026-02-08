namespace MotorsportsApp.Models.Domain;

public class Circuit
{
    public string CircuitId { get; set; } = string.Empty;
    public string CircuitName { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string Locality { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
