using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LichessNET.Converters;

public class MillisecondsDateTimeConverter : DateTimeConverterBase
{
    internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        writer.WriteRawValue(((DateTime)value - UnixEpoch).TotalMilliseconds.ToString());
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        if (reader.Value == null) { return null; }
        return UnixEpoch.AddMilliseconds((long)reader.Value);
    }
}