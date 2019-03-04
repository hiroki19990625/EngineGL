using System.Threading.Tasks;
using MessagePack;

namespace EngineGL.FormatMessage
{
    public static class NetworkMessage
    {
        public static byte[] ToBinary<T>(this T obj)
        {
            return MessagePackSerializer.Serialize(obj);
        }

        public static Task<byte[]> ToBinaryAsync<T>(this T obj)
        {
            return Task.Run(() => obj.ToBinary());
        }

        public static T FromBinary<T>(this byte[] binaryData)
        {
            return MessagePackSerializer.Deserialize<T>(binaryData);
        }

        public static Task<T> FromBinaryAsync<T>(this byte[] binaryData)
        {
            return Task.Run(() => binaryData.FromBinary<T>());
        }
    }
}