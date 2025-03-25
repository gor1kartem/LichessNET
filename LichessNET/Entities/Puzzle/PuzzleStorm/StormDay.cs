using System.Text.Json.Serialization;
using LichessNET.Converters;

namespace LichessNET.Entities.Puzzle.PuzzleStorm;

public class StormDay
{
    [JsonPropertyName("_id")]
    [JsonConverter(typeof(DateOnlyIsoConverter))]
    public DateOnly? Date { get; set; }
    public int Combo { get; set; }
    public int Errors { get; set; }
    public int Highest { get; set; }
    public int Moves { get; set; }
    public int Runs { get; set; }
    public int Score { get; set; }
    public int Time { get; set; }
}