using LichessNET.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;
using System.Web;

namespace LichessNET.API.Users
{
    internal partial class UsersAPIFunctions
    {
        internal static async Task<LichessUser> GetPublicUserData(string username, UriBuilder builder)
        {

            string url = builder.ToString();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (!CheckRequest(response, url)) return null;

                string s = await response.Content.ReadAsStringAsync();
                logger.LogDebug("Response: " + s);
                var dynamicObject = JsonConvert.DeserializeObject<dynamic>(s);
                if (dynamicObject == null)
                {
                    logger.LogError("Failed to deserialize response from " + url);
                    return null;
                }

                

                var LichessUser = new LichessUser()
                {
                    Username = dynamicObject.username,
                    ID = dynamicObject.id,
                    Flair = dynamicObject.flair,
                    Disabled = dynamicObject.disabled,
                    Blocking = dynamicObject.blocking,
                    Count = dynamicObject.count.ToObject<GameCounts>(),
                    Followable = dynamicObject.followable,
                    Following = dynamicObject.following,
                    FollowsYou = dynamicObject.followsYou,
                    Patron = dynamicObject.patron,
                    Streaming = dynamicObject.streaming,
                    TOSViolation = dynamicObject.tosViolation,
                    title = dynamicObject.title,
                    Verified = dynamicObject.verified,
                    PlayTime = dynamicObject.playTime.ToObject<PlaytimeStats>(),
                    Profile = dynamicObject.profile.ToObject<LichessProfile>(),
                    Ratings = Converter.UserConverters.PerfsConverter(dynamicObject.perfs),
                };



                return LichessUser;
            }
        }



    }
}
