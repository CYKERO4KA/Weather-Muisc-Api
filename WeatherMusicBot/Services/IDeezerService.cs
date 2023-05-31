namespace WeatherMusicBot.Services;

public interface IDeezerService
{
    Task<string> SearchPlaylist(string playlistName);
    Task<string> SearchTrack(string trackName);
}