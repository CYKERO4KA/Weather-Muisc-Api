using Microsoft.AspNetCore.Mvc;
using WeatherMusicBot.Services;

namespace WeatherMusicBot.Controllers;

[ApiController]
[Route("[controller]")]
public class YouTubeController : ControllerBase
{
    private IYouTubeMusicService _youTubeMusicService;
    private IConfiguration _configuration;
    private string? _apiKey;
    public YouTubeController(IConfiguration configuration)
    {
        _configuration = configuration;
        _apiKey = configuration.GetValue<string>("YouTube:Token");
        _youTubeMusicService = new YouTubeMusicMusicService();
    }

    [HttpGet("searchPlaylist/{playlistName}")]
    public string GetYouTubePlaylist(string playlistName)
    {
        var response = _youTubeMusicService.SearchPlaylist(playlistName, _apiKey);
        
        return response;
    }
    [HttpGet("searchTrack/{trackName}")]
    public string GetYouTubeTrack(string trackName)
    {
        var response = _youTubeMusicService.SearchTrack(trackName, _apiKey);
        
        return response;
    }
}