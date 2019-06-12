using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EngineGL.Serializations
{
    public static class JsonSerialization
    {
        public static string ToJson<T>(this T obj, bool indented = false)
        {
            return JsonConvert.SerializeObject(obj, indented ? Formatting.Indented : Formatting.None);
        }

        public static Task<string> ToJsonAsync<T>(this T obj, bool indented = false)
        {
            return Task.Run(() => obj.ToJson(indented));
        }

        public static string ToDeserializableJson<T>(this T obj, bool indented = false)
        {
            return JsonConvert.SerializeObject(obj, indented ? Formatting.Indented : Formatting.None,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
        }

        public static Task<string> ToDeserializableJsonAsync<T>(this T obj, bool indented = false)
        {
            return Task.Run(() => obj.ToDeserializableJson(indented));
        }

        public static T FromDeserializableJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }

        public static Task<T> FromDeserializableJsonAsync<T>(this string json)
        {
            return Task.Run(() => json.FromDeserializableJson<T>());
        }
    }
}