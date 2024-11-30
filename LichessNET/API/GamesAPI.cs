using LichessNET.Entities.Game;
using Microsoft.Extensions.Logging;

namespace LichessNET.API;

public partial class LichessApiClient
{
    public async Task<Game> GetGame(string gameId)
    {
        var request = GetRequestScaffold("game/export/" + gameId);

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        return Game.FromPgn(content);
    }

    public async Task<List<Game>> GetGames(string username, int max = 10)
    {
        var request = GetRequestScaffold("api/games/user/" + username,
            Tuple.Create("max", max.ToString()));

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        var list = new List<Game>();

        var gamepgns = content.Split("\n\n\n");
        foreach (var gamepgn in gamepgns)
        {
            try
            {
                if (gamepgn.Length < 10) continue;
                list.Add(Game.FromPgn(gamepgn.Trim()));
            }
            catch (Exception e)
            {
                _logger.LogWarning("Failed to parse a pgn: " + gamepgn);
                throw;
            }
        }

        return list;
    }
}