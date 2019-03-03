using EngineGL.Core.LifeCycle;
using OpenTK;

namespace EngineGL.Core
{
    public interface ICamera : IUpdateable
    {
        Matrix4 LookAtMatrix { get; }
    }
}