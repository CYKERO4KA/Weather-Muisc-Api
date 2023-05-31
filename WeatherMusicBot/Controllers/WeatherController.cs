using Microsoft.AspNetCore.Mvc;
using WeatherMusicBot.Services;
using Microsoft.Extensions.Configuration;

namespace WeatherMusicBot.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IOpenWeatherMapService _openWeatherMapService;
    private readonly IConfiguration _configuration;

    public WeatherController(IConfiguration configuration)
    {
        _configuration = configuration;
        var apiKey = configuration.GetValue<string>("OpenWeatherMap:Token");
        _openWeatherMapService = new OpenWeatherMapClient(apiKey);
    }

    [HttpGet("{lat}/{lon}")]
    public async Task<ActionResult> Get(double lat, double lon)
    {
        var response = await _openWeatherMapService.GetTemperatureAsync(lat, lon);

        if (response == "Clear")
            return Ok("upbeat");
        else if (response == "Rain" || response == "Drizzle")
            return Ok("mellow");
        else
            return Ok("neutral");

        //return Ok($"{response}");
    }
}