namespace LichessNET.API;

public partial class LichessAPIClient
{
    public async Task<string> GetGame(string gameId)
    {
        var request = GetRequestScaffold("game/export/" + gameId);

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        return content;
    }
}