using System.Net;
using System.Net.Http.Headers;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities;
using LichessNET.Extensions;
using Microsoft.Extensions.Logging;
using System.Web;
using Newtonsoft.Json;
using OpeningMentor.Chess.Model;
using TokenBucket;
using Vertical.SpectreLogger;


namespace LichessNET.API
{
    /// <summary>
    /// This class represents a client for the lichess API.
    /// It handles all ratelimits and requests.
    /// </summary>
    public partial class LichessAPIClient
    {
        private ILogger logger;

        /// <summary>
        /// Bucket handler for ratelimits
        /// </summary>
        APIRatelimitController ratelimitController = new APIRatelimitController();

        /// <summary>
        /// The token to access the Lichess API
        /// </summary>
        internal string Token = "";

        /// <summary>
        /// Creates a lichess API client, according to settings
        /// </summary>
        /// <param name="Token">The token for accessing the lichess API</param>
        public LichessAPIClient(string Token = "")
        {
            var loggerFactory = LoggerFactory.Create(builder => builder
                .AddFilter(level => level >= LogLevel.Trace)
                .AddSpectreConsole());

            logger = loggerFactory.CreateLogger("LichessAPIClient");


            this.Token = Token;
            if (Token != "")
            {
                logger.LogInformation("Connecting to Lichess API with token");
            }
            else
            {
                logger.LogInformation("Connecting to Lichess API without token");
            }

            if (!Token.Contains("_"))
            {
                logger.LogWarning("The token provided may not be a valid lichess API token. Please check the token.");
            }

            logger.LogInformation("Connection to Lichess API established.");
        }

        public async Task<LichessUser> GetOwnProfile()
        {
            var request = GetRequestScaffold("api/account");
            var response = SendRequest(request);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LichessUser>(content);
        }

        public async Task<List<UserRealTimeStatus>> GetRealTimeUserStatus(string id, bool withSignal = false,
            bool withGameIds = false, bool withGameMetas = false)
        {
            return await GetRealTimeUserStatus(new List<string> { id }, withSignal, withGameIds, withGameMetas);
        }


        /// <summary>
        /// Gets the UriBuilder objects for the lichess client.
        /// If something changes in the future, it will be easy to change it.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private UriBuilder GetUriBuilder(string endpoint, params Tuple<string, string>[] QueryParameters)
        {
            var builder = new UriBuilder(Constants.BASE_URL + endpoint);
            builder.Port = -1;

            var query = HttpUtility.ParseQueryString(builder.Query);

            foreach (var param in QueryParameters)
            {
                query[param.Item1] = param.Item2;
            }

            builder.Query = query.ToString();

            return builder;
        }

        private HttpRequestMessage GetRequestScaffold(string endpoint, params Tuple<string, string>[] QueryParameters)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = GetUriBuilder(endpoint, QueryParameters).Uri;
            return request;
        }

        private HttpResponseMessage SendRequest(HttpRequestMessage request)
        {
            ratelimitController.Consume(request.RequestUri.AbsolutePath, true);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            logger.LogInformation("Sending request to " + request.RequestUri);
            var response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                logger.LogDebug("Request to " + request.RequestUri + " successful.");
                logger.LogDebug("Response: \n" + response.Content.ReadAsStringAsync().Result);
                return response;
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    logger.LogError("Ratelimited by Lichess API. Waiting for 60 seconds.");
                    ratelimitController.ReportBlock(60);
                    return null;
                }
                else
                {
                    logger.LogError("Error while fetching data from Lichess API. Status code: " + response.StatusCode);
                    logger.LogInformation("Response: \n" + response.Content.ReadAsStringAsync().Result);
                    return null;
                }
            }
        }
    }
}