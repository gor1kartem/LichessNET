using System.Text.Json.Serialization;
using LichessNET.Converters;

namespace LichessNET.Entities.Social;

public class Note
{
    public UserOverview From { get; set; }
    public UserOverview To { get; set; }
    public string Text { get; set; }
    [JsonConverter(typeof(MillisecondUnixConverter))]
    public DateTime Date { get; set; }
}