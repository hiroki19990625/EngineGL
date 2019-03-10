using EngineGL.Utils;

namespace EngineGL.Structs.Secure
{
    public abstract class SecureValue<T> : ISecureValue<T>
    {
        private byte[] _secureValue;

        public byte[] Seed { get; private set; }

        public T Value
        {
            get
            {
                byte[] data = (byte[]) _secureValue.Clone();
                return FromSecure(data);
            }
        }

        public SecureValue(T value)
        {
            Init(value);
        }

        public virtual void Init(T value)
        {
            byte[] buffer = ToSecure(value);
            Seed = new byte[buffer.Length];
            LocalThreadRandom.GetRandom().NextBytes(Seed);
            _secureValue = this.Xor(buffer);
        }

        public virtual void Set(T value)
        {
            Init(value);
        }

        public abstract byte[] ToSecure(T value);
        public abstract T FromSecure(byte[] secure);
    }
}