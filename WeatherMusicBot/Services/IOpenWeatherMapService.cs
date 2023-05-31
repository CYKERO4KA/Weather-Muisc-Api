namespace WeatherMusicBot.Services;

public interface IOpenWeatherMapService
{
    Task<string> GetTemperatureAsync(double lat, double lon);
}