using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherMusicBot.Services;

public class DeezerService : IDeezerService
{
    readonly HttpClient client = new HttpClient();
    
    public async Task<string> SearchPlaylist(string playlistName)
    {
        try
        {
            string url = $"https://api.deezer.com/search/playlist?q={Uri.EscapeDataString(playlistName)}";

            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            
            DeezerResponse deezerResponse = JsonConvert.DeserializeObject<DeezerResponse>(responseBody);
            
            if (deezerResponse.Data != null && deezerResponse.Data.Count > 0)
            {
                return deezerResponse.Data[0].Link;
            }

            return null;
        }
        catch (HttpRequestException e)
        {
            return $"Error: {e.Message}";
        }
    }

    public async Task<string> SearchTrack(string trackName)
    {
        try
        {
            string url = $"https://api.deezer.com/search/track?q={Uri.EscapeDataString(trackName)}";

            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            DeezerResponse deezerResponse = JsonConvert.DeserializeObject<DeezerResponse>(responseBody);

            if (deezerResponse.Data != null && deezerResponse.Data.Count > 0)
            {
                return deezerResponse.Data[0].Link;
            }

            return null;
        }
        catch (HttpRequestException e)
        {
            return $"Error: {e.Message}";
        }
    }

}
public class DeezerResponse
{
    public List<DeezerData> Data { get; set; }
}

public class DeezerData
{
    public string Link { get; set; }
}
