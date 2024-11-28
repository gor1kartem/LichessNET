using LichessNET.Entities;
using Newtonsoft.Json;

namespace LichessNET.API;

public partial class LichessAPIClient
{
    public async Task<List<UserRealTimeStatus>> GetRealTimeUserStatus(IEnumerable<string> ids, bool withSignal = false,
        bool withGameIds = false, bool withGameMetas = false)
    {
        var request = GetRequestScaffold("api/users/status",
            Tuple.Create("ids", string.Join(",", ids)),
            Tuple.Create("withSignal", withSignal.ToString().ToLower()),
            Tuple.Create("withGameIds", withGameIds.ToString().ToLower()),
            Tuple.Create("withGameMetas", withGameMetas.ToString().ToLower())
        );

        var response = SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        var userStatuses = JsonConvert.DeserializeObject<List<UserRealTimeStatus>>(content);
        return userStatuses;
    }
}