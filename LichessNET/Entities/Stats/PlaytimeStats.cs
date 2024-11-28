namespace LichessNET.Entities;

public class PlaytimeStats
{
    public int total;
    public int tv;

    /// <summary>
    ///     The total time played by the user
    /// </summary>
    public TimeSpan TotalSpan => TimeSpan.FromSeconds(total);

    /// <summary>
    ///     The total time seen on TV
    /// </summary>
    public TimeSpan TVSpan => TimeSpan.FromSeconds(tv);
}