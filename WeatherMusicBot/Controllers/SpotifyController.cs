using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using WeatherMusicBot.Models;
using WeatherMusicBot.Services;

namespace WeatherMusicBot.Controllers;

[ApiController]
[Route("[controller]")]
public class SpotifyController : ControllerBase
{
    private readonly ISpotifyAccountServices _spotifyAccountServices;
    private SpotifyClient _client;
    private readonly IConfiguration _configuration;
    private ISpotifyService _spotifyService;
    private string clientId;
    private string clientSecret;
    
    public SpotifyController(IConfiguration configuration)
    {
        _configuration = configuration;
        _spotifyAccountServices = new SpotifyAccountService();
        _spotifyService = new SpotifyService(_spotifyAccountServices);
        
        clientId = _configuration.GetValue<string>("Spotify:ClientId");
        clientSecret = _configuration.GetValue<string>("Spotify:ClientSecret");
    }
    
    [HttpGet("searchPlaylist/{playlistName}")]
    public async Task<string> GetPlaylist(string playlistName)
    {
        var responseTask = _spotifyService.SearchPlaylist(clientId, clientSecret, playlistName);

        if (responseTask == null)
            throw new InvalidOperationException("_spotifyService.SearchPlaylist returned null Task");

        var response = await responseTask;

        return response ?? "Playlist not found.";
    }
    
    [HttpGet("searchTrack/{trackName}")]
    public async Task<string> GetSong(string trackName)
    {
        var responseTask = _spotifyService.SearchTrack(clientId, clientSecret, trackName);

        if (responseTask == null)
            throw new InvalidOperationException("_spotifyService.SearchPlaylist returned null Task");

        var response = await responseTask;

        return response ?? "Track not found.";
    }
}