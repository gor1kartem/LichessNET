using LichessNET.Entities;
using LichessNET.Entities.Account;
using Newtonsoft.Json;

namespace LichessNET.API;

public partial class LichessAPIClient
{
    /// <summary>
    ///     Retrieves the email address of the authenticated user.
    /// </summary>
    /// <returns>
    ///     A string representing the email address of the authenticated user.
    /// </returns>
    public async Task<string> GetAccountEmail()
    {
        ratelimitController.Consume("api/account", false);

        var request = GetRequestScaffold("api/account/email");

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        var emailResponse = JsonConvert.DeserializeObject<dynamic>(content);
        return emailResponse.email.ToObject<string>();
    }

    public async Task<LichessUser> GetOwnProfile()
    {
        ratelimitController.Consume("api/account", false);

        var request = GetRequestScaffold("api/account");
        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<LichessUser>(content);
    }

    public async Task<AccountPreferences> GetAccountPreferences()
    {
        ratelimitController.Consume("api/account", false);

        var request = GetRequestScaffold("api/account/preferences");

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();


        var preferences = JsonConvert.DeserializeObject<AccountPreferences>(content);
        return preferences;
    }

    /// <summary>
    ///     Determines whether the authenticated user's account is in kid mode.
    /// </summary>
    /// <returns>
    ///     A boolean value indicating if the user's account is set to kid mode.
    /// </returns>
    public async Task<bool> GetKidModeStatus()
    {
        ratelimitController.Consume("api/account", false);

        var request = GetRequestScaffold("api/account/kid");

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        var kidModeStatus = JsonConvert.DeserializeObject<dynamic>(content).kid.ToObject<bool>();
        return kidModeStatus;
    }

    public async Task<bool> SetKidModeStatus(bool enable)
    {
        ratelimitController.Consume("api/account", false);

        var request = GetRequestScaffold("api/account/kid", Tuple.Create("v", enable.ToString()));
        var response = await SendRequest(request, HttpMethod.Post);

        return JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()).ok.ToObject<bool>();
    }
}