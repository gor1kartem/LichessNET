namespace LichessNET.Entities.Stats;

public class PlaytimeStats
{
    public int Total;
    public int Tv;

    /// <summary>
    ///     The total time played by the user
    /// </summary>
    public TimeSpan TotalSpan => TimeSpan.FromSeconds(Total);

    /// <summary>
    ///     The total time seen on TV
    /// </summary>
    public TimeSpan TvSpan => TimeSpan.FromSeconds(Tv);
}