namespace WeatherMusicBot.Services;

public interface IYouTubeMusicService
{
    string SearchPlaylist(string playlistName, string apiKey);
    string SearchTrack(string trackName, string apiKey);
}