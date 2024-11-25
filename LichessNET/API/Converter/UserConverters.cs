using LichessNET.Entities;
using LichessNET.Entities.Enumerations;
using Newtonsoft.Json.Linq;

namespace LichessNET.API.Converter
{
    internal class UserConverters
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static Dictionary<Gamemode, GamemodeStats> PerfsConverter(dynamic obj)
        {
            var ratings = new Dictionary<Gamemode, GamemodeStats>();
            Dictionary<string, GamemodeStats> native = obj.ToObject<Dictionary<string, GamemodeStats>>();

            foreach (var n in native)
            {
                ratings.Add(ParseEnum<Gamemode>(n.Key), n.Value);
            }

            return ratings;
        }
    }
}
