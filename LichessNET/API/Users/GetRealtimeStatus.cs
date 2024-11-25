using LichessNET.Entities;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LichessNET.API.Users
{
    internal partial class UsersAPIFunctions
    {

        [Obsolete("This function is not yet correctly implemented")]
        internal static async Task<List<UserRealTimeStatus>> GetRealTimeStatus(UriBuilder builder, string[] ids, bool withSignal = false, bool withGameIds = false, bool withGameMetas = false)
        {
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["ids"] = string.Join(',', ids);
            query["withSignal"] = withSignal.ToString();
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

                List<UserRealTimeStatus> users = new List<UserRealTimeStatus>();

                var dynamicObject = JsonConvert.DeserializeObject<dynamic>(s);
                if (dynamicObject == null)
                {
                    logger.LogError("Failed to deserialize response from " + url);
                    return null;
                }
                foreach(var d in dynamicObject.ToObject<JObject>())
                {
                    try
                    {
                        var user = d.ToObject<UserRealTimeStatus>();
                        users.Add(user);
                    }
                    catch(Exception ex)
                    {
                        logger.LogError("Failed to deserialize response from " + url + ": " + ex.Message + "\nObject Data:\n{object}", (object)d);
                    }

                    

                }


                return users;

            }

        }

        internal static async Task<UserRealTimeStatus> GetRealTimeStatus(UriBuilder builder, string id, bool withSignal = false, bool withGameIds = false, bool withGameMetas = false)
        {
            //var builder = new UriBuilder(Constants.BASE_URL + "api/users/status");
            //builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["ids"] = id;
            query["withSignal"] = withSignal.ToString();
            builder.Query = query.ToString();
            string url = builder.ToString();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if(!CheckRequest(response, url)) return null;
                string s = await response.Content.ReadAsStringAsync();
                if (s == null)
                {
                    logger.LogError("Failed to deserialize response from " + url);
                    return null;
                }
                logger.LogDebug("Response: " + s);
                var dynamicObject = JsonConvert.DeserializeObject<dynamic>(s)[0];
                if (dynamicObject == null)
                {
                    logger.LogError("Failed to deserialize response from " + url);
                    return null;
                }
                try
                {
                    UserRealTimeStatus user = new UserRealTimeStatus()
                    {
                        Online = dynamicObject.online,
                        Playing = dynamicObject.playing,
                        Streaming = dynamicObject.streaming,
                        Patron = dynamicObject.patron,
                        User = new LichessUser()
                        {
                            Patron = dynamicObject.patron,
                            title = dynamicObject.title,
                            ID = dynamicObject.id,
                            Username = dynamicObject.name,
                        }
                    };

                    return user;
                }
                catch
                {
                    UserRealTimeStatus user = new UserRealTimeStatus()
                    {
                        Online = false,
                        Playing = false,
                        Streaming = false,
                        Patron = false,
                        User = new LichessUser()
                        {
                            ID = dynamicObject.id,
                            Username = dynamicObject.name,
                        }
                    };

                    return user;
                }

            }

        }
    }
}
