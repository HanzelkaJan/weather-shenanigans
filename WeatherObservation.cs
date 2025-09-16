namespace WeatherShenanigans;

public class WeatherObservation
{
    public int Id { get; set; }
    public string? Json { get; set; }
    public DateTime DlTime { get; set; }
    public string? Error { get; set; }
}