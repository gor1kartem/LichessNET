namespace LichessNET.Entities.Game;

public class Clock
{
    public int Initial { get; set; }
    public int Increment { get; set; }
    public int totalTime { get; set; }

    public TimeSpan InitialTime => TimeSpan.FromSeconds(Initial);

    public TimeSpan IncrementTime => TimeSpan.FromSeconds(Increment);

    public TimeSpan TotalTime => TimeSpan.FromSeconds(totalTime);
}