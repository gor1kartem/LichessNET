using LichessNET.API.Users;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities;
using LichessNET.Extensions;
using Microsoft.Extensions.Logging;
using System.Web;
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
        /// The token to access the Lichess API
        /// </summary>
        internal string Token = "";
        
        /// <summary>
        /// Bucket handler for ratelimits
        /// </summary>
        APIRatelimitController ratelimitController = new APIRatelimitController();

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
                logger.LogWarning("Authentication with Token is not yet implemented. Keep an eye on any updates");
            }
            else
            {
                logger.LogInformation("Connecting to Lichess API without token");
            }

            ratelimitController.RegisterBucket("api/users/status", TokenBuckets.Construct().WithCapacity(2).WithFixedIntervalRefillStrategy(1, TimeSpan.FromSeconds(5)).Build());

            logger.LogInformation("Connection to Lichess API established.");
        }

        /// <summary>
        /// Gets the public profile of a lichess
        /// </summary>
        /// <param name="username">The username of the user to fetch the profile of</param>
        /// <returns>A LichessUser obeject with all information sent by lichess</returns>
        public async Task<LichessUser> GetPublicProfile(string username)
        {
            var builder = GetUriBuilder("api/user/" + username);
            ratelimitController.Consume("api/user/", true);
            logger.LogInformation("Requesting to " + builder.Uri.AbsoluteUri);
            return await UsersAPIFunctions.GetPublicUserData(username, builder);
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
            var builder = GetUriBuilder("api/users/status");
            ratelimitController.Consume("api/users/status", true);
            logger.LogInformation("Requesting to " + builder.Uri.AbsoluteUri);
            return await UsersAPIFunctions.GetRealTimeStatus(builder, id, withSignal);
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
            var builder = GetUriBuilder("api/users/status");
            ratelimitController.Consume("api/users/status", true);
            logger.LogInformation("Requesting to " + builder.Uri.AbsoluteUri + " (" + ids.Length + " ids)");
            return await UsersAPIFunctions.GetRealTimeStatus(builder, ids, withSignal);
        }
        
        /// <summary>
        /// Gets the Leaderboard of a certain gamemode.
        /// </summary>
        /// <param name="nPlayers">How many players should be included in the list</param>
        /// <param name="gamemode">The gamemode to request the leaderboard from.</param>
        /// <returns>A list of LichessUser objects with all included information.</returns>
        public async Task<List<LichessUser>> GetLeaderboard(int nPlayers, Gamemode gamemode)
        {
            var builder = GetUriBuilder($"api/player/top/{nPlayers}/{gamemode.ToEnumMember()}");
            ratelimitController.Consume("api/player/top", true);
            logger.LogInformation("Requesting to " + builder.Uri.AbsoluteUri);
            return await UsersAPIFunctions.GetLeaderboard(builder, nPlayers, gamemode);
        }

        /// <summary>
        /// Gets the UriBuilder objects for the lichess client.
        /// If something changes in the future, it will be easy to change it.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private UriBuilder GetUriBuilder(string endpoint)
        {
            var builder = new UriBuilder(Constants.BASE_URL + endpoint);
            builder.Port = -1;
            return builder;
        }

    }
}
