using System.Text.Json.Serialization;
using LichessNET.Entities.Enumerations;

namespace LichessNET.Entities.Social.Stream;

public class LiveStreamer
{
    public string Id { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Title Title { get; set; }
    public bool Patron { get; set; }
    public Stream Stream { get; set; }
    public Streamer Streamer { get; set; }
}