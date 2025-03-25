using System.Text.Json.Serialization;

namespace LichessNET.Entities.Teams;

public class TeamPaginator
{
    public int CurrentPage { get; set; }
    public int MaxPerPage { get; set; }
    public int? PreviousPage { get; set; }
    public int NextPage { get; set; }
    /// <summary>
    /// Total number of teams on Lichess
    /// </summary>
    [JsonPropertyName("nbResults")]
    public int NumberOfResults { get; set; }
    /// <summary>
    /// Total number of pages
    /// </summary>
    [JsonPropertyName("nbPages")]
    public int NumberOfPages { get; set; }
    [JsonPropertyName("currentPageResults")]
    public List<PopularTeam> Results { get; set; }
}