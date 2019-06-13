using EngineGL.Core;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.Components
{
    public class Collision2D : Component, ICollision
    {
        public Vec3 Bounds { get; set; }
        public Vec3 Offset { get; set; }

        public Collision2D()
        {
        }
    }
}