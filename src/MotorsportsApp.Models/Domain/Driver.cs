namespace MotorsportsApp.Models.Domain;

public class Driver
{
    public string DriverId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string PermanentNumber { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public string FamilyName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string Nationality { get; set; } = string.Empty;
    public string TeamId { get; set; } = string.Empty;
    
    public string FullName => $"{GivenName} {FamilyName}";
}
