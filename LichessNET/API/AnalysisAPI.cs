using LichessNET.Entities.Analysis;
using LichessNET.Entities.Enumerations;
using LichessNET.Extensions;
using Newtonsoft.Json;

namespace LichessNET.API;

public partial class LichessApiClient
{
    public async Task<PositionEvaluation> GetCachedEvaluationAsync(string fen, int multiPv = 1,
        ChessVariant variant = ChessVariant.Standard)
    {
        _ratelimitController.Consume();

        var endpoint = $"api/cloud-eval";
        var request = GetRequestScaffold(endpoint,
            Tuple.Create("fen", fen),
            Tuple.Create("multiPv", multiPv.ToString()),
            Tuple.Create("variant", variant.GetEnumMemberValue()));

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        var evaluationResponse = JsonConvert.DeserializeObject<PositionEvaluation>(content);
        return evaluationResponse;
    }
}