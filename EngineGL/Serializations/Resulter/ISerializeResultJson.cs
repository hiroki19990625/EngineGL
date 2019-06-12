using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EngineGL.Serializations.Resulter
{
    public interface ISerializeResultJson : ISerializeResult
    {
        JObject OnSerializeJson();
        void OnDeserializeJson(JObject obj);
    }
}