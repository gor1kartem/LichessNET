using LichessNET.API.Users;
using LichessNET.Entities;
using Microsoft.Extensions.Logging;
using System.Web;
using TokenBucket;
using Vertical.SpectreLogger;

namespace LichessNET.API
{
    public partial class LichessAPIClient
    {

        private ILogger logger;
        internal string Token = "";
        APIRatelimitController ratelimitController = new APIRatelimitController();

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

        public async Task<LichessUser> GetPublicProfile(string username)
        {
            var builder = GetUriBuilder("api/user/" + username);
            ratelimitController.Consume("api/user/", true);
            logger.LogInformation("Requesting to " + builder.Uri.AbsoluteUri);
            return await UsersAPIFunctions.GetPublicUserData(username, builder);
        }

        public async Task<UserRealTimeStatus> GetRealTimeStatus(string id, bool withSignal)
        {
            var builder = GetUriBuilder("api/users/status");
            ratelimitController.Consume("api/users/status", true);
            logger.LogInformation("Requesting to " + builder.Uri.AbsoluteUri);
            return await UsersAPIFunctions.GetRealTimeStatus(builder, id, withSignal);
        }

        public async Task<List<UserRealTimeStatus>> GetRealTimeStatus(string[] ids, bool withSignal)
        {
            var builder = GetUriBuilder("api/users/status");
            ratelimitController.Consume("api/users/status", true);
            logger.LogInformation("Requesting to " + builder.Uri.AbsoluteUri + " (" + ids.Length + " ids)");
            return await UsersAPIFunctions.GetRealTimeStatus(builder, ids, withSignal);
        }

        public UriBuilder GetUriBuilder(string endpoint)
        {
            var builder = new UriBuilder(Constants.BASE_URL + endpoint);
            builder.Port = -1;
            return builder;
        }

    }
}
