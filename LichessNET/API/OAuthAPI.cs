using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.OAuth;

namespace LichessNET.API;

public partial class LichessApiClient
{
    public async Task<Dictionary<string, TokenInfo?>> TestTokensAsync(List<string> tokens)
    {
        var tokenBody = string.Join(',', tokens);
        var request = GetRequestScaffold("api/token/test");
        var response = await SendRequest(request, content: new StringContent(tokenBody), method: HttpMethod.Post);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        var tokenInfo = await response.Content.ReadFromJsonAsync<Dictionary<string, TokenInfo>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
        return tokenInfo;
    }

    public async Task DeleteTokenAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var request = GetRequestScaffold("api/token");
        var response = await SendRequest(request, method: HttpMethod.Delete);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
    }
}