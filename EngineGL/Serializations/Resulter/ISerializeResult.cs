namespace EngineGL.Serializations.Resulter
{
    public interface ISerializeResult
    {
        void OnSerialize();
        void OnDeserialize<T>(T data);
    }
}