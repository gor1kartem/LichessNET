using LichessNET.Entities.Game;
using Microsoft.Extensions.Logging;

namespace LichessNET.API;

public partial class LichessApiClient
{
    /// <summary>
    /// Retrieves a chess game using its unique identifier from the Lichess API.
    /// </summary>
    /// <param name="gameId">The unique identifier of the game to retrieve.</param>
    public async Task<Game> GetGameAsync(string gameId)
    {
        var request = GetRequestScaffold("game/export/" + gameId);

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        return Game.FromPgn(content);
    }

    /// <summary>
    /// Retrieves a list of chess games for a specified user from the Lichess API.
    /// </summary>
    /// <param name="username">The username of the player whose games are to be retrieved.</param>
    /// <param name="max">The maximum number of games to retrieve. Default is 10.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of games.</returns>
    public async Task<List<Game>> GetGamesAsync(string username, int max = 10)
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

    /// <summary>
    /// Retrieves a list of chess games that have been imported to the Lichess platform.
    /// </summary>
    /// <returns>A list of imported chess games.</returns>
    public async Task<List<Game>> GetImportedGamesAsync()
    {
        var request = GetRequestScaffold("api/games/export/import");
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


    /// <summary>
    /// Initializes a real-time stream of a chess game using its unique identifier from the Lichess API.
    /// </summary>
    /// <param name="gameId">The unique identifier of the game to stream.</param>
    /// <returns>A GameStream object that provides updates as the game progresses.</returns>
    public async Task<GameStream> GetGameStreamAsync(string gameId)
    {
        return new GameStream(gameId);
    }
}