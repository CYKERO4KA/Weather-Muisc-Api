using Microsoft.AspNetCore.Mvc;
using WeatherMusicBot.Services;

namespace WeatherMusicBot.Controllers;

[ApiController]
[Route("[controller]")]
public class DeezerController : ControllerBase
{
    private readonly IDeezerService _deezerService;

    public DeezerController()
    {
        _deezerService = new DeezerService();
    }

    [HttpGet("searchPlaylist/{playlistName}")]
    public async Task<IActionResult> SearchPlaylist(string playlistName)
    {
        try
        {
            string response = await _deezerService.SearchPlaylist(playlistName);
            if (response == null)
                return Ok("Playlist not found.");
            else
                return Ok(response);
        }
        catch
        {
            return BadRequest("Error occurred while searching for playlist.");
        }
    }
    [HttpGet("searchTrack/{trackName}")]
    public async Task<IActionResult> SearchTrack(string trackName)
    {
        try
        {
            string response = await _deezerService.SearchTrack(trackName);
            if (response == null)
                return Ok("Track not found.");
            else
                return Ok(response);
        }
        catch
        {
            return BadRequest("Error occurred while searching for playlist.");
        }
    }
}