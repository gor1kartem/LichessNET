using LichessNET.API.Converter;
using LichessNET.Entities.Enumerations;

namespace LichessNET.Entities.Game;

public class Game
{
    public string Id { get; set; }
    public bool Rated { get; set; }
    public string Variant { get; set; }
    public string Speed { get; set; }
    public string Perf { get; set; }
    public long CreatedAt { get; set; }

    public DateTime CreatedAtTimestamp
    {
        get
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds( CreatedAt ).ToLocalTime();
            return dateTime;
        }
    }
    
    public long LastMoveAt { get; set; }
    
    public DateTime LastMoveTimestamp
    {
        get
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(LastMoveAt).ToLocalTime();
            return dateTime;
        }
    }

    public string Status { get; set; }
    public Player White { get; set; }
    public Player Black { get; set; }
    public Opening? Opening { get; set; }
    public string? Moves { get; set; }
    public Clock? Clock { get; set; }
}