namespace EngineGL.Resource
{
    public interface IRefCounter
    {
        void Ref();
        void UnRef();

        bool IsNotRef();
    }
}