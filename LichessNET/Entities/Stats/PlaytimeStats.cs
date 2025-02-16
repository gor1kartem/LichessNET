using LichessNET.Converters;
using Newtonsoft.Json;

namespace LichessNET.Entities.Stats;

public class PlaytimeStats
{
    [JsonConverter(typeof(IntToSecondsTimeSpanConverter))]
    public TimeSpan? Total;
    [JsonConverter(typeof(IntToSecondsTimeSpanConverter))]
    public TimeSpan? Tv;
}