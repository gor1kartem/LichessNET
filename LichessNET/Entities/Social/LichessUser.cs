using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Stats;

namespace LichessNET.Entities.Social;

/// <summary>
///     The complete information of a lichess user.
///     This class also contains information on which the client doesn't have access to.
///     In this case, the client will return null on such attributes.
/// </summary>
public class LichessUser
{
    /// <summary>
    ///     User ID, often it is the Username written in lowercase
    /// </summary>
    public string Id { get; set; } = String.Empty;

    /// <summary>
    ///     The username of the user
    /// </summary>
    public string Username { get; set; } = "Anonymous";

    /// <summary>
    ///     If the data is fetched in the request, the ratings will be set here.
    ///     The dictionary only contains those gamemodes as key, which were fetched.
    /// </summary>
    public IReadOnlyDictionary<Gamemode, GamemodeStats>? Ratings { get; set; }

    /// <summary>
    ///     Current flair of the user
    /// </summary>
    public string? Flair { get; set; }

    private ulong? CreatedAt { get; set; }

    /// <summary>
    ///     Will be set to true if the user profile is disabled
    /// </summary>
    public bool? Disabled { get; set; }

    /// <summary>
    ///     Will be set to true if the account is flagged for TOS violations
    /// </summary>
    public bool? TosViolation { get; set; }

    /// <summary>
    ///     The LichessProfile of the user
    /// </summary>
    public LichessProfile? Profile { get; set; }

    private ulong? SeenAt { get; set; }

    /// <summary>
    ///     If set to true, this user is an active patron of lichess
    /// </summary>
    public bool? Patron { get; set; }

    /// <summary>
    ///     Set to true if the user is a verfied user
    /// </summary>
    public bool? Verified { get; set; }

    /// <summary>
    ///     The total playtime
    /// </summary>
    public PlaytimeStats? PlayTime { get; set; }

    /// <summary>
    ///     Title of this user as string
    /// </summary>
    internal string? title { get; set; }

    /// <summary>
    ///     The Title as an enumeration.
    ///     If the user has no title, Title.None will be returned
    /// </summary>
    public Title Title
    {
        get
        {
            switch (title)
            {
                case "CM":
                    return Title.Cm;
                case "IM":
                    return Title.Im;
                case "GM":
                    return Title.Gm;
                case "LM":
                    return Title.Lm;
                default:
                    return Title.None;
            }
        }
    }

    /// <summary>
    ///     The game count stats
    /// </summary>
    public GameCounts? Count { get; set; }

    /// <summary>
    ///     Returns if the user is streaming
    /// </summary>
    public bool? Streaming { get; set; }

    /// <summary>
    ///     The streaming information about the user (channels, etc.)
    /// </summary>
    public StreamingInfo? Streamer { get; set; }

    /// <summary>
    ///     Set to true if the user allows being followed
    /// </summary>
    public bool? Followable { get; set; }

    /// <summary>
    ///     Set to true if the user is following
    /// </summary>
    public bool? Following { get; set; }

    /// <summary>
    ///     If the user blocks the request
    /// </summary>
    public bool? Blocking { get; set; }

    /// <summary>
    ///     Set to true if the user follows the user represented by the LichessClient
    /// </summary>
    public bool? FollowsYou { get; set; }
}