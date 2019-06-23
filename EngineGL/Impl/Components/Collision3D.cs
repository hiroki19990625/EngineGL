using EngineGL.Core;
using EngineGL.Core.Components;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.Components
{
    public class Collision3D : Component, ICollision
    {
        public Vec3 Bounds { get; set; }
        public Vec3 Offset { get; set; }

        public Collision3D()
        {
        }
    }
}