using SpotifyAPI.Web;


namespace WeatherMusicBot.Services;

public class SpotifyAccountService : ISpotifyAccountServices
{
    public async Task<string> GetToken(string? clientId, string? clientSecret)
    {
        var config = SpotifyClientConfig.CreateDefault();
        var request = new ClientCredentialsRequest(clientId, clientSecret);

        try
        {
            var response = await new OAuthClient(config).RequestToken(request);
            return response.AccessToken;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    

}