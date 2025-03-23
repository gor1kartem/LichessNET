using System.Text.Json.Serialization;
using LichessNET.Converters;
using LichessNET.Entities.Enumerations;

namespace LichessNET.Entities.Stats;

public class LeaderboardUserOverview
{
    public string Id { get; set; }
    public string Username { get; set; }
    public bool Online { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Title? Title { get; set; }
    [JsonConverter(typeof(LeaderboardStatsConverter))]
    public Dictionary<Gamemode, LeaderboardStats> Perfs { get; set; }
}

public struct LeaderboardStats
{
    public int Rating { get; set; }
    public int Progress { get; set; }
}