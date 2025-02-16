
using Newtonsoft.Json;

namespace LichessNET.Converters;
public class IntToSecondsTimeSpanConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        if (objectType == typeof(TimeSpan?))
        {
            var value = reader.Value.ToString();
            if (reader.Value == null)
            {
                return null;
            }
            return TimeSpan.FromSeconds(int.Parse(reader.Value.ToString()));
        }
        return null;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(int);
    }
}