using SpotifyAPI.Web;

namespace WeatherMusicBot.Services;

public interface ISpotifyService
{
    Task<string> SearchPlaylist(string clientId, string clientSecret, string playlistName);
    Task<string> SearchTrack(string? clientId, string? clientSecret, string trackName);
    Task<FullPlaylist> CreatePlaylist(string userId, string playlistName, string clientId, string clientSecret);
    Task<bool> AddItemsToPlaylist(string playlistId, List<string> trackUris, string clientId, string clientSecret);
}