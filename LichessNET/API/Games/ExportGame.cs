using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using LichessNET.API.Converter;
using LichessNET.Entities.Game;
using Microsoft.Extensions.Logging;
using Vertical.SpectreLogger;


namespace LichessNET.API.Games
{
    public partial class GamesAPIFunctions
    {
        public static async Task<Game> FetchGame(HttpRequestMessage request)
        {
            string url = request.RequestUri.ToString();
            logger.LogInformation("Requesting to " + url);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (!CheckRequest(response, url)) return null;

                string s = await response.Content.ReadAsStringAsync();
                logger.LogInformation(s);

                // Deserialize the response content into a dynamic object
                dynamic game = JsonConvert.DeserializeObject<dynamic>(s);

                return GameConverters.ConvertGame(game);
            }
        }
    }
}