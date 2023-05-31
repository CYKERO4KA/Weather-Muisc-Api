using SpotifyAPI.Web;
using WeatherMusicBot.Models;

namespace WeatherMusicBot.Services;

public class SpotifyService : ISpotifyService
{
    private ISpotifyAccountServices _spotifyAccountServices;

    public SpotifyService(ISpotifyAccountServices spotifyAccountServices)
    {
        _spotifyAccountServices = spotifyAccountServices;
    }
    public async Task<string> SearchTrack(string clientId, string clientSecret, string trackName)
    {
        string accessToken = await _spotifyAccountServices.GetToken(clientId, clientSecret);
        var config = SpotifyClientConfig.CreateDefault()
            .WithAuthenticator(new TokenAuthenticator(accessToken, "Bearer"));
        var spotify = new SpotifyClient(config);

        var request = new SearchRequest(SearchRequest.Types.Track, trackName);
        var searchResult = await spotify.Search.Item(request);

        if (searchResult.Tracks.Items.Count > 0)
        {
            return searchResult.Tracks.Items[0].ExternalUrls["spotify"];
        }

        return null; 
    }
    public async Task<string> SearchPlaylist(string clientId, string clientSecret, string playlistName)
    {
        string accessToken = await _spotifyAccountServices.GetToken(clientId, clientSecret);
        var config = SpotifyClientConfig.CreateDefault()
            .WithAuthenticator(new TokenAuthenticator( accessToken, "Bearer"));
        var spotify = new SpotifyClient(config);

        var request = new SearchRequest(SearchRequest.Types.Playlist, playlistName);
        var searchResult = await spotify.Search.Item(request);

        if (searchResult.Playlists.Items.Count > 0)
        {
            return searchResult.Playlists.Items[0].ExternalUrls["spotify"];
        }

        return null; 
    }
    public async Task<FullPlaylist> CreatePlaylist(string userId, string playlistName, string clientId, string clientSecret)
    {
        string accessToken = await _spotifyAccountServices.GetToken(clientId, clientSecret);
        var config = SpotifyClientConfig.CreateDefault()
            .WithAuthenticator(new TokenAuthenticator(accessToken, "Bearer"));
        var spotify = new SpotifyClient(config);

        var request = new PlaylistCreateRequest(playlistName);
        return await spotify.Playlists.Create(userId, request);
    }

    public async Task<bool> AddItemsToPlaylist(string playlistId, List<string> trackUris, string clientId, string clientSecret)
    {
        string accessToken = await _spotifyAccountServices.GetToken(clientId, clientSecret);
        var config = SpotifyClientConfig.CreateDefault()
            .WithAuthenticator(new TokenAuthenticator(accessToken, "Bearer"));
        var spotify = new SpotifyClient(config);

        var request = new PlaylistAddItemsRequest(trackUris);
        var result = await spotify.Playlists.AddItems(playlistId, request);
    
        return true;
    }

}