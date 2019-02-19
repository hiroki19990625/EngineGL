namespace EngineGL.Core
{
    public interface IObject
    {
        void OnStart();
        bool OnUpdate();
        void OnDestroy();
    }
}