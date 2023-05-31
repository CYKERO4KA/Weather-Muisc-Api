namespace WeatherMusicBot.Services;

public interface ISpotifyAccountServices
{
    Task<string> GetToken(string? clientId, string? clientSecret);
}