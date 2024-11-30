using LichessNET.Entities;
using LichessNET.Entities.Account;
using LichessNET.Entities.Social;
using Newtonsoft.Json;

namespace LichessNET.API;

public partial class LichessApiClient
{
    /// <summary>
    ///     Retrieves the email address of the authenticated user.
    /// </summary>
    /// <returns>
    ///     A string representing the email address of the authenticated user.
    /// </returns>
    public async Task<string> GetAccountEmail()
    {
        _ratelimitController.Consume("api/account", false);

        var request = GetRequestScaffold("api/account/email");

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        var emailResponse = JsonConvert.DeserializeObject<dynamic>(content);
        return emailResponse.email.ToObject<string>();
    }

    /// <summary>
    /// Retrieves the profile information of the authenticated user from the Lichess platform.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="LichessUser"/> representing the profile details of the user.
    /// </returns>
    public async Task<LichessUser> GetOwnProfile()
    {
        _ratelimitController.Consume("api/account", false);

        var request = GetRequestScaffold("api/account");
        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<LichessUser>(content);
    }

    /// <summary>
    /// Retrieves the account preferences of the authenticated user.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="AccountPreferences"/> representing the user's account preferences.
    /// </returns>
    public async Task<AccountPreferences> GetAccountPreferences()
    {
        _ratelimitController.Consume("api/account", false);

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
        _ratelimitController.Consume("api/account", false);

        var request = GetRequestScaffold("api/account/kid");

        var response = await SendRequest(request);
        var content = await response.Content.ReadAsStringAsync();

        var kidModeStatus = JsonConvert.DeserializeObject<dynamic>(content).kid.ToObject<bool>();
        return kidModeStatus;
    }

    /// <summary>
    /// Sets the kid mode status for the authenticated account.
    /// </summary>
    /// <param name="enable">
    /// A boolean value indicating whether to enable or disable kid mode.
    /// </param>
    /// <returns>
    /// A boolean indicating whether the operation was successful.
    /// </returns>
    public async Task<bool> SetKidModeStatus(bool enable)
    {
        _ratelimitController.Consume("api/account", false);

        var request = GetRequestScaffold("api/account/kid", Tuple.Create("v", enable.ToString()));
        var response = await SendRequest(request, HttpMethod.Post);

        return JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()).ok.ToObject<bool>();
    }
}