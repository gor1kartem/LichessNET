using System;
using LichessNET.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LichessNET.API.Account;

public partial class AccountAPIFunctions
{

    public static async Task<LichessUser> GetOwnProfile(HttpRequestMessage request)
    {
        string url = request.RequestUri.ToString();
        logger.LogInformation("Requesting to " + url);
            
            
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
                
            var lichessUser = new LichessUser()
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



            return lichessUser;
        }
    }
    
}