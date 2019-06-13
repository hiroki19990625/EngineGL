using EngineGL.Structs.Math;

namespace EngineGL.Core
{
    public interface ICollision
    {
        Vec3 Bounds { get; set; }
        Vec3 Offset { get; set; }
    }
}