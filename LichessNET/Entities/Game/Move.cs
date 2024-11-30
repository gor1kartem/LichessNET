namespace LichessNET.Entities.Game;

public class Move
{
    public int MoveNumber { get; set; }
    public bool IsWhite { get; set; }
    public string Notation { get; set; }
    public TimeSpan Clock { get; set; }
    public float Evaluation { get; set; }
}