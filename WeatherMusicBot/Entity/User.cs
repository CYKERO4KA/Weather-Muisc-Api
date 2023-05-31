namespace WeatherMusicBot.Entity;

public class User 
{
    public long ChatId { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}