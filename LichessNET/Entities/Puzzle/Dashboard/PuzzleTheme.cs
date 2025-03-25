using System.Text.Json.Serialization;

namespace LichessNET.Entities.Puzzle.Dashboard;

public class PuzzleTheme
{
    public PuzzleResults Results { get; set; }
    [JsonPropertyName("theme")]
    public string ThemeName { get; set; }
}