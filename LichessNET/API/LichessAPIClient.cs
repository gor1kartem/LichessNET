using System.Net.Http.Headers;
using LichessNET.API.Users;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities;
using LichessNET.Extensions;
using Microsoft.Extensions.Logging;
using System.Web;
using LichessNET.API.Account;
using LichessNET.API.Games;
using OpeningMentor.Chess.Model;
using TokenBucket;
using Vertical.SpectreLogger;
using Game = LichessNET.Entities.Game.Game;


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

            ratelimitController.RegisterBucket("api/users/status",
                TokenBuckets.Construct().WithCapacity(2).WithFixedIntervalRefillStrategy(1, TimeSpan.FromSeconds(5))
                    .Build());

            logger.LogInformation("Connection to Lichess API established.");
        }

        /// <summary>
        /// Gets the public profile of a lichess
        /// </summary>
        /// <param name="username">The username of the user to fetch the profile of</param>
        /// <returns>A LichessUser obeject with all information sent by lichess</returns>
        public async Task<LichessUser> GetPublicProfile(string username)
        {
            ratelimitController.Consume("api/user/", true);
            return await UsersAPIFunctions.GetPublicUserData(GetRequestScaffold("api/user/" + username));
        }

        /// <summary>
        /// Gets the Current Real Time Status of a specific user.
        /// To fetch the status of several user, use the overloaded function
        /// </summary>
        /// <param name="id">The ID of the user, often it's the username in lower case.</param>
        /// <param name="withSignal">Should the current signal strength be included in the request? Takes longer if set to yes</param>
        /// <returns>An object with all Real Time Data, including a LichessUser object with all information additionaly sent</returns>
        public async Task<UserRealTimeStatus> GetRealTimeStatus(string id, bool withSignal)
        {
            ratelimitController.Consume("api/users/status", true);
            return await UsersAPIFunctions.GetRealTimeStatus(
                GetRequestScaffold("api/users/status",
                    new Tuple<string, string>("ids", id),
                    new Tuple<string, string>("withSignal", withSignal.ToString())));
        }

        /// <summary>
        /// Returns the real time status of several users. It's faster to request several users at once.
        /// If you only want to request one user, it is more convenient to use the overload of the method.
        /// </summary>
        /// <param name="ids">Array of IDs to request the Real Time Status of. Can contain up to 100 IDs</param>
        /// <param name="withSignal">Should the current signal strength be included in the request? It takes longer if it is included</param>
        /// <returns>A list of UserRealTimeStatus objects containing all information.</returns>
        public async Task<List<UserRealTimeStatus>> GetRealTimeStatus(string[] ids, bool withSignal)
        {
            ratelimitController.Consume("api/users/status", true);
            return await UsersAPIFunctions.GetMultipleRealTimeStatus(
                GetRequestScaffold("api/users/status",
                    new Tuple<string, string>("ids", string.Join(",", ids)),
                    new Tuple<string, string>("withSignal", withSignal.ToString())));
        }

        /// <summary>
        /// Gets the Leaderboard of a certain gamemode.
        /// </summary>
        /// <param name="nPlayers">How many players should be included in the list</param>
        /// <param name="gamemode">The gamemode to request the leaderboard from.</param>
        /// <returns>A list of LichessUser objects with all included information.</returns>
        public async Task<List<LichessUser>> GetLeaderboard(int nPlayers, Gamemode gamemode)
        {
            ratelimitController.Consume("api/player/top", true);
            return await UsersAPIFunctions.GetLeaderboard
            (GetRequestScaffold($"api/player/top/{nPlayers}/{gamemode.ToEnumMember()}"),
                nPlayers,
                gamemode);
        }

        public async Task<LichessUser> GetOwnProfile()
        {
            ratelimitController.Consume("api/player/top", true);
            return await AccountAPIFunctions.GetOwnProfile(GetRequestScaffold($"api/account"));
        }

        public async Task<Game> GetGame(string gameId, bool withMoves = true)
        {
            ratelimitController.Consume("api/game", true);
            return await GamesAPIFunctions.FetchGame(GetRequestScaffold($"api/game/{gameId}",
                new Tuple<string, string>("moves", withMoves.ToString()),
                new Tuple<string, string>("pgnInJson", true.ToString())
            ));
        }

        public async Task<Database> GetGames(string username)
        {
            ratelimitController.Consume($"api/games/user/{username}", true);
            return await GamesAPIFunctions.FetchGames(GetRequestScaffold($"api/games/user/{username}"));
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
            if (Token != "")
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }

            return request;
        }
    }
}