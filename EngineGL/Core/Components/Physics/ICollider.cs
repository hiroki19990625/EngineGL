using EngineGL.Structs.Math;
using Jitter.Collision.Shapes;

namespace EngineGL.Core.Components.Physics
{
    public interface ICollider : IComponent
    {
        Shape ColliderShape { get; }
        Vec3 Offset { get; set; }
    }
}