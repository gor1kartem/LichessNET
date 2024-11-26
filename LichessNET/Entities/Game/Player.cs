namespace LichessNET.Entities.Game;

public class Player
{
    public string? Id { get; set; }
    public bool? Provisional { get; set; }
    public int? Rating { get; set; }
    public int? RatingDiff { get; set; }
}