namespace EngineGL.Structs.Secure
{
    public interface ISecureValue<T> where T : struct
    {
        byte[] Seed { get; }
        T Value { get; }

        byte[] ToSecure(T value);
        T FromSecure(byte[] secure);
    }
}