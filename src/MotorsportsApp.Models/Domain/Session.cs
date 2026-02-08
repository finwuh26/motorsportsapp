using MotorsportsApp.Models.Enums;

namespace MotorsportsApp.Models.Domain;

public class Session
{
    public string SessionId { get; set; } = string.Empty;
    public int Season { get; set; }
    public int Round { get; set; }
    public SessionType SessionType { get; set; }
    public string RaceName { get; set; } = string.Empty;
    public Circuit? Circuit { get; set; }
    public DateTime? SessionDate { get; set; }
    public DateTime? SessionTime { get; set; }
    public FlagStatus CurrentFlag { get; set; }
    public bool IsLive { get; set; }
}
