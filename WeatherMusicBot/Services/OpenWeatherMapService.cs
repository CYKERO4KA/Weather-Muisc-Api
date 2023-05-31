using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherMusicBot.Services
{
    public class OpenWeatherMapClient : IOpenWeatherMapService
    {
        private readonly string _apiKey;

        public OpenWeatherMapClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> GetTemperatureAsync(double lat, double lon)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={_apiKey}";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            return (string)JObject.Parse(response)["weather"][0]["main"];
            //return response;
        }

    }
}
