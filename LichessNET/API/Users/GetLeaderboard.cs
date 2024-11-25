using LichessNET.Entities.Enumerations;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LichessNET.Entities;

namespace LichessNET.API.Users
{
    internal partial class UsersAPIFunctions
    {

        public static async Task<List<LichessUser>> GetLeaderboard(UriBuilder builder, int nPlayers, Gamemode gamemode)
        {
            var query = HttpUtility.ParseQueryString(builder.Query);
            builder.Query = query.ToString();
            string url = builder.ToString();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (!CheckRequest(response, url)) return null;
                string s = await response.Content.ReadAsStringAsync();
                if (s == null)
                {
                    logger.LogError("Failed to deserialize response from " + url);
                    return null;
                }
                logger.LogInformation("Response: " + s);

                List<LichessUser> users = new List<LichessUser>();

                var dynamicObject = JsonConvert.DeserializeObject<dynamic>(s);
                if (dynamicObject == null)
                {
                    logger.LogError("Failed to deserialize response from " + url);
                    return null;
                }
                foreach (var d in dynamicObject.users.ToObject<JArray>())
                {
                    var user = new LichessUser
                    {
                        title = d.title,
                        Username = d.username,
                        ID = d.id,
                        Ratings = Converter.UserConverters.PerfsConverter(d.perfs)
                    };


                    users.Add(user);

                }


                return users;

            }
        }

    }
}
