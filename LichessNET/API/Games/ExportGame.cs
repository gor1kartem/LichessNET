using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LichessNET.API.Converter;
using Microsoft.Extensions.Logging;
using Vertical.SpectreLogger;
using OpeningMentor.Chess;
using OpeningMentor.Chess.Model;
using OpeningMentor.Chess.Pgn;

namespace LichessNET.API.Games
{
    public partial class GamesAPIFunctions
    {
        /// <summary>
        /// Asynchronously fetches a game from a given HTTP request message.
        /// </summary>
        /// <param name="request">The HTTP request message used to fetch the game, containing the request URL and any necessary headers.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the fetched <see cref="Game"/> object, or null if the request was unsuccessful.</returns>
        public static async Task<Entities.Game.Game> FetchGame(HttpRequestMessage request)
        {
            //request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-chess-pgn");
            string url = request.RequestUri.ToString();
            logger.LogInformation("Requesting to " + url);

            request.Method = HttpMethod.Get;

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

        public static async Task<Database> FetchGames(HttpRequestMessage request)
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
                //dynamic games = JsonConvert.DeserializeObject<dynamic>(s);

                var parser = new PgnReader();
                var games = parser.ReadFromString(s);
                return games;
            }
        }
    }
}