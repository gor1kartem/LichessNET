namespace LichessNET.Entities.Game;

public class MoveSequence
{
    public List<Move> Moves { get; set; } = new List<Move>();
    public string OriginalPGN { get; set; }
}