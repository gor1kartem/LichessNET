namespace LichessNET.Entities.Social;

public class Matchup
{
    public int TotalGames { get; set; }
    public Dictionary<string, int> Scores { get; set; }
}