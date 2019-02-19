using EngineGL.Core.LifeCycle;

namespace EngineGL.Core
{
    public interface IObject : Initialzeable, IDestroyable, IUpdateable, INameable
    {
        string Tag { get; set; }
    }
}