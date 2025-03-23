using System.Text.Json;
using System.Text.Json.Serialization;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Stats;

namespace LichessNET.Converters;

public class LeaderboardStatsConverter : JsonConverter<Dictionary<Gamemode, LeaderboardStats>>
{
    public override Dictionary<Gamemode, LeaderboardStats>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var output = new Dictionary<Gamemode, LeaderboardStats>();
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            foreach (JsonProperty property in doc.RootElement.EnumerateObject())
            {
                if (Gamemode.TryParse(property.Name, true, out Gamemode gamemode))
                {
                    var stats = property.Value.Deserialize<LeaderboardStats>(new JsonSerializerOptions()
                        { PropertyNameCaseInsensitive = true });
                    output[gamemode] = stats;
                }
            }
        }
        return output;
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<Gamemode, LeaderboardStats> value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}