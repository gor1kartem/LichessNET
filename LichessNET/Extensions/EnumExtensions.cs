using System.Reflection;
using System.Runtime.Serialization;

namespace LichessNET.Extensions
{
    internal static class EnumExtensions
    {
        public static string? ToEnumMember<T>(this T value) where T : Enum
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())?
                .GetCustomAttribute<EnumMemberAttribute>(false)?
                .Value;
        }
    }
}
