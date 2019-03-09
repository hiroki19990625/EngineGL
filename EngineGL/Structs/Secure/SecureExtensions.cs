namespace EngineGL.Structs.Secure
{
    public static class SecureExtensions
    {
        public static byte[] Xor<T>(this ISecureValue<T> secureValue, byte[] buffer) where T : struct
        {
            byte[] seed = secureValue.Seed;
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte) (buffer[i] ^ seed[i]);
            }

            return buffer;
        }
    }
}