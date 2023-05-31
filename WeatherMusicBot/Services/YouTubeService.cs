namespace WeatherMusicBot.Services;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

public class YouTubeMusicMusicService : IYouTubeMusicService
{
    public string SearchPlaylist(string playlistName, string apiKey)
    {
        YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
        {
            ApiKey = apiKey,
            ApplicationName = "TelegramMusic Bot"
        });
        
        var searchRequest = youtubeService.Search.List("snippet");
        searchRequest.Q = playlistName;
        searchRequest.Type = "playlist";
        searchRequest.MaxResults = 1;

        var searchResponse = searchRequest.Execute();
        if (searchResponse.Items.Count > 0)
        {
            var playlistId = searchResponse.Items[0].Id.PlaylistId;
            var playlistUrl = $"https://www.youtube.com/playlist?list={playlistId}";
            return playlistUrl;
        }
        return ("Playlist not found.");;
    }

    public string SearchTrack(string trackName, string apiKey)
    {
        YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
        {
            ApiKey = apiKey,
            ApplicationName = "TelegramMusic Bot"
        });
    
        var searchRequest = youtubeService.Search.List("snippet");
        searchRequest.Q = trackName;
        searchRequest.Type = "video";
        searchRequest.MaxResults = 1;

        var searchResponse = searchRequest.Execute();
        if (searchResponse.Items.Count > 0)
        {
            var videoId = searchResponse.Items[0].Id.VideoId;
            var videoUrl = $"https://www.youtube.com/watch?v={videoId}";
            return videoUrl;
        }
        return ("Track not found.");
    }

}