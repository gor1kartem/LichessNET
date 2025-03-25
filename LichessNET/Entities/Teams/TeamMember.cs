using System.Text.Json.Serialization;
using LichessNET.Entities.Enumerations;

namespace LichessNET.Entities.Teams;

/// <summary>
/// Represents a member of a Lichess team.
/// </summary>
public class TeamMember
{
    public string ID { get; set; } = null!;
    public string Username { get; set; } = null!;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Title? Title { get; set; } 
    public bool Patron { get; set; }
    public ulong joinedTeamAt { get; set; }
}