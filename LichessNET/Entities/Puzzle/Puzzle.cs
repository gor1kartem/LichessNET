namespace LichessNET.Entities.Puzzle;

/// <summary>
/// Represents a chess puzzle obtained from Lichess, including its metadata and solution.
/// </summary>
public class Puzzle
{
    public string id { get; set; }
    public PuzzleGame Game { get; set; }

    public int InitialPly { get; set; }
    public int Plays { get; set; }
    public int Rating { get; set; }
    public List<string> Solution { get; set; } = new List<string>();
    public List<string> Themes { get; set; } = new List<string>();
}