using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace EngineGL.Serializations
{
    public static class YamlSerialization
    {
        public static string ToYaml<T>(this T obj)
        {
            SerializerBuilder sb = new SerializerBuilder()
                .EmitDefaults();
            Serializer s = (Serializer) sb.Build();
            return s.Serialize(obj);
        }

        public static Task<string> ToYamlAsync<T>(this T obj)
        {
            return Task.Run(() => obj.ToYaml());
        }

        public static T FromYaml<T>(this string yaml)
        {
            Deserializer db = (Deserializer) new DeserializerBuilder().Build();
            return db.Deserialize<T>(yaml);
        }

        public static Task<T> FromYamlAsync<T>(this string yaml)
        {
            return Task.Run(() => yaml.FromYaml<T>());
        }
    }
}