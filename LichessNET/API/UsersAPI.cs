using LichessNET.Entities;
using Newtonsoft.Json;

namespace LichessNET.API;

public partial class LichessAPIClient
{
    /// <summary>
    ///     Retrieves the real-time status of a specified user on Lichess.
    /// </summary>
    /// <param name="id">The ID of the user whose real-time status is to be retrieved.</param>
    /// <param name="withSignal">Optional parameter to include the connectivity signal status of the user.</param>
    /// <param name="withGameIds">Optional parameter to include the IDs of the games the user is playing.</param>
    /// <param name="withGameMetas">Optional parameter to include meta information about the games the user is involved in.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, containing the real-time status of the user.
    /// </returns>
    public async Task<UserRealTimeStatus> GetRealTimeUserStatus(string id, bool withSignal = false,
        bool withGameIds = false, bool withGameMetas = false)
    {
        return (await GetRealTimeUserStatus(new List<string> { id }, withSignal, withGameIds, withGameMetas))[0];
    }

    /// <summary>
    ///     Retrieves the real-time status of specified users on Lichess.
    /// </summary>
    /// <param name="ids">A collection of user IDs whose real-time statuses are to be retrieved.</param>
    /// <param name="withSignal">Optional parameter to include the connectivity signal status of users.</param>
    /// <param name="withGameIds">Optional parameter to include the IDs of the games users are playing.</param>
    /// <param name="withGameMetas">Optional parameter to include meta information about the games users are involved in.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, containing a list of user real-time statuses.
    /// </returns>
    public async Task<List<UserRealTimeStatus>> GetRealTimeUserStatus(IEnumerable<string> ids, bool withSignal = false,
        bool withGameIds = false, bool withGameMetas = false)
    {
        ratelimitController.Consume();

        var request = GetRequestScaffold("api/users/status",
            Tuple.Create("ids", string.Join(",", ids)),
            Tuple.Create("withSignal", withSignal.ToString().ToLower()),
            Tuple.Create("withGameIds", withGameIds.ToString().ToLower()),
            Tuple.Create("withGameMetas", withGameMetas.ToString().ToLower())
        );

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        var userStatuses = JsonConvert.DeserializeObject<List<UserRealTimeStatus>>(content);
        return userStatuses;
    }
}