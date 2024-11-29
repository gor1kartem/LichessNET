using LichessNET.Entities.Game;

namespace LichessNET.API;

public partial class LichessAPIClient
{
    public async Task<Game> GetGame(string gameId)
    {
        var request = GetRequestScaffold("game/export/" + gameId);

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        return Game.FromPGN(content);
    }
}