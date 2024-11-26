using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertical.SpectreLogger;

namespace LichessNET.API.Users
{
    internal partial class UsersAPIFunctions
    {
        private static ILogger logger = LoggerFactory.Create(builder => builder.SetMinimumLevel(Constants.MinimumLogLevel)
                   .AddSpectreConsole()).CreateLogger("Users API");

        /// <summary>
        /// Checks if a response is okay.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool CheckRequest(HttpResponseMessage response, string uri)
        {
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    APIRatelimitController.ReportBlock();
                    logger.LogError("Lichess blocked request: Ratelimit exceeded.");
                    return false;
                }
                else
                {
                    logger.LogError("Unsuccessful request to endpoint " + uri + ": Response Code " + response.StatusCode);
                    return false;
                }
               
            }
            return true;
        }

    }
}
