using LichessNET.Entities.Puzzle;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LichessNET.API;

public partial class LichessApiClient
{
    /// <summary>
    /// Retrieves the daily puzzle from the Lichess API.
    /// </summary>
    /// <returns>
    /// A <see cref="Puzzle"/> object representing the daily puzzle, including associated game data and solution themes.
    /// </returns>
    public async Task<Puzzle> GetDailyPuzzle()
    {
        _ratelimitController.Consume();

        var request = GetRequestScaffold("api/puzzle/daily");
        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();
        var jobj = JsonConvert.DeserializeObject<JObject>(content);
        var puzzle = jobj["puzzle"].ToObject<Puzzle>();
        puzzle.Game = jobj["game"].ToObject<PuzzleGame>();
        return puzzle;
    }

    /// <summary>
    /// Fetches a random chess puzzle from the Lichess API.
    /// </summary>
    /// <returns>
    /// A <see cref="Puzzle"/> object representing the random chess puzzle, including its associated game details.
    /// </returns>
    public async Task<Puzzle> GetRandomPuzzle()
    {
        _ratelimitController.Consume();

        var request = GetRequestScaffold("api/puzzle/next");
        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();
        var jobj = JsonConvert.DeserializeObject<JObject>(content);
        var puzzle = jobj["puzzle"].ToObject<Puzzle>();
        puzzle.Game = jobj["game"].ToObject<PuzzleGame>();
        return puzzle;
    }

    /// <summary>
    /// Retrieves a specific chess puzzle from the Lichess API using its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the puzzle to retrieve.</param>
    /// <returns>A <see cref="Puzzle"/> object containing the details of the requested puzzle, including its game data and solution themes.</returns>
    public async Task<Puzzle> GetPuzzleByID(string id)
    {
        _ratelimitController.Consume();

        var request = GetRequestScaffold($"api/puzzle/{id}");
        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();
        var jobj = JsonConvert.DeserializeObject<JObject>(content);
        var puzzle = jobj["puzzle"].ToObject<Puzzle>();
        puzzle.Game = jobj["game"].ToObject<PuzzleGame>();
        return puzzle;
    }
}