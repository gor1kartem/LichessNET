using LichessNET.Entities.Teams;
using LichessNET.Entities.Tournament;
using Newtonsoft.Json;

namespace LichessNET.API;

public partial class LichessApiClient
{
    /// <summary>
    ///     Get the team with the specified ID
    /// </summary>
    /// <param name="teamId">The ID of the team</param>
    /// <returns>The team with the specified ID</returns>
    public async Task<LichessTeam> GetTeamAsync(string teamId)
    {
        _ratelimitController.Consume();
        var request = GetRequestScaffold($"api/team/{teamId}");
        var response = await SendRequest(request);

        var content = await response.Content.ReadAsStringAsync();
        var team = JsonConvert.DeserializeObject<LichessTeam>(content);

        return team;
    }

    public async Task<List<LichessTeam>> GetPopularTeamsAsync(int page = 1)
    {
        _ratelimitController.Consume();
        var request = GetRequestScaffold("api/team/all", new Tuple<string, string>("page", page.ToString()));
        var response = await SendRequest(request);

        var content = await response.Content.ReadAsStringAsync();
        var teamspage = JsonConvert.DeserializeObject<dynamic>(content);

        return teamspage["currentPageResults"].ToObject<List<LichessTeam>>();
    }

    /// <summary>
    /// Get the list of teams that the specified user is in.
    /// </summary>
    /// <param name="username">The username of the user</param>
    /// <returns>A list of teams that the specified user is in</returns>
    public async Task<List<LichessTeam>> GetTeamOfUserAsync(string username)
    {
        _ratelimitController.Consume();

        var request = GetRequestScaffold($"api/team/of/{username}");
        var response = await SendRequest(request);

        var content = await response.Content.ReadAsStringAsync();
        var teams = JsonConvert.DeserializeObject<List<LichessTeam>>(content);

        return teams;
    }

    /// <summary>
    /// This funciton gets the members of a team, in chronoclogical order of joining the team. (Latest first),
    /// with up to 5000 members.
    /// </summary>
    /// <param name="teamId">ID of the Team</param>
    /// <returns></returns>
    public async Task<List<TeamMember>> GetTeamMembersAsync(string teamId)
    {
        _ratelimitController.Consume();
        var request = GetRequestScaffold($"api/team/{teamId}/users");

        var response = await SendRequest(request);

        var content = await response.Content.ReadAsStringAsync();
        var members = JsonConvert.DeserializeObject<List<TeamMember>>(content);

        return members;
    }

    /// <summary>
    /// Retrieves all Swiss tournaments of a team.
    /// </summary>
    /// <param name="teamId">The ID of the team.</param>
    /// <param name="max">The maximum number of tournaments to download. Default is 100.</param>
    /// <returns>A task representing the asynchronous operation, containing the list of Swiss tournaments.</returns>
    public async Task<List<SwissTournament>> GetTeamSwissTournamentsAsync(string teamId, int max = 100)
    {
        _ratelimitController.Consume();

        var endpoint = $"api/team/{teamId}/swiss?max={max}";
        var request = GetRequestScaffold(endpoint);

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        var tournaments = new List<SwissTournament>();
        using (var reader = new StringReader(content))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var tournament = JsonConvert.DeserializeObject<SwissTournament>(line);
                tournaments.Add(tournament);
            }
        }

        return tournaments;
    }
}