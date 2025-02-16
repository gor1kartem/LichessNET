using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Stats;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LichessNET.Converters;

public class GamemodeStatsConverter : JsonConverter<Dictionary<Gamemode, IGameStats>>
{
    public override void WriteJson(JsonWriter writer, Dictionary<Gamemode, IGameStats>? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override Dictionary<Gamemode, IGameStats>? ReadJson(JsonReader reader, Type objectType, Dictionary<Gamemode, IGameStats>? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }
        Console.WriteLine(objectType.ToString());
        Dictionary<Gamemode, IGameStats> dictionary = new();
        JObject jObject = JObject.Load(reader);
        foreach (var item in jObject)
        {
            Gamemode mode;
            if (Gamemode.TryParse(item.Key, true, out mode))
            {
                if (mode == Gamemode.Storm || mode == Gamemode.Racer || mode == Gamemode.Streak)
                {
                    dictionary.Add(mode, item.Value.ToObject<PuzzleStats>());
                }
                else
                {
                    dictionary.Add(mode, item.Value.ToObject<GamemodeStats>());
                }
            }
        }

        return dictionary;
    }
}