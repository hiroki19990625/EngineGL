using System.Threading.Tasks;
using MessagePack;

namespace EngineGL.Serializations
{
    public static class NetworkSerialization
    {
        public static byte[] ToBinary<T>(this T obj)
        {
            return MessagePackSerializer.Serialize(obj);
        }

        public static Task<byte[]> ToBinaryAsync<T>(this T obj)
        {
            return Task.Run(() => obj.ToBinary());
        }

        public static byte[] ToCompressBinary<T>(this T obj)
        {
            return LZ4MessagePackSerializer.Serialize(obj);
        }

        public static Task ToCompressBinaryAsync<T>(this T obj)
        {
            return Task.Run(() => ToCompressBinary(obj));
        }

        public static T FromBinary<T>(this byte[] binaryData)
        {
            return MessagePackSerializer.Deserialize<T>(binaryData);
        }

        public static Task<T> FromBinaryAsync<T>(this byte[] binaryData)
        {
            return Task.Run(() => binaryData.FromBinary<T>());
        }

        public static T FromCompressBinary<T>(this byte[] binaryData)
        {
            return LZ4MessagePackSerializer.Deserialize<T>(binaryData);
        }

        public static Task FromCompressBinaryAsync<T>(this byte[] binaryData)
        {
            return Task.Run(() => binaryData.FromCompressBinary<T>());
        }
    }
}