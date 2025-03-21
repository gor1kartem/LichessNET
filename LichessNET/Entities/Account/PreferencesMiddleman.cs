namespace LichessNET.Entities.Account;

/// <summary>
///     Represents the account preferences of a user on Lichess.
/// </summary>
internal class PreferencesMiddleman
{
    public AccountPreferences Prefs { get; set; }
    public string Language { get; set; }
}