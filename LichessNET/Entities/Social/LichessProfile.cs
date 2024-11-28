namespace LichessNET.Entities;

/// <summary>
///     This class contains all information of the public profile of a lichess user
/// </summary>
public class LichessProfile
{
    /// <summary>
    ///     The current country flag of the user
    /// </summary>
    private string Flag { get; set; }

    /// <summary>
    ///     The set location of this user
    /// </summary>
    private string Location { get; set; }

    /// <summary>
    ///     The bio of the user
    /// </summary>
    private string Bio { get; set; }

    /// <summary>
    ///     The set real name of the user
    /// </summary>
    private string RealName { get; set; }

    /// <summary>
    ///     FIDE rating of the user
    /// </summary>
    private ushort FideRating { get; set; }

    /// <summary>
    ///     USCF rating of the user
    /// </summary>
    private ushort UsCfRating { get; set; }

    /// <summary>
    ///     ECF rating of the user
    /// </summary>
    private ushort EcfRating { get; set; }

    /// <summary>
    ///     CFC rating of the user
    /// </summary>
    private ushort CfcRating { get; set; }

    /// <summary>
    ///     DSB rating of the user
    /// </summary>
    private ushort DsbRating { get; set; }

    /// <summary>
    ///     Links mentioned in the bio of the user
    ///     Each link is seperated by \r\n
    /// </summary>
    private string Links { get; set; }
}