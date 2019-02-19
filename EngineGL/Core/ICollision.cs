namespace EngineGL.Core
{
    public interface ICollision : ITransform
    {
        bool OnCollisionEnter();
        bool OnCollisionStay();
        bool OnCollisionLeave();
    }
}