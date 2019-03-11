namespace EngineGL.Structs.Secure
{
    public interface ISecureValue<T>
    {
        byte[] Seed { get; }
        T Value { get; }

        void Set(T value);
    }
}