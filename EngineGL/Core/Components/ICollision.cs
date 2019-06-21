using EngineGL.Structs.Math;

namespace EngineGL.Core.Components
{
    public interface ICollision : IComponent
    {
        Vec3 Bounds { get; set; }
        Vec3 Offset { get; set; }
    }
}