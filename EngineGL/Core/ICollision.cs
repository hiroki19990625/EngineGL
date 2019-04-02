using System;
using EngineGL.Structs.Math;

namespace EngineGL.Core
{
    public interface ICollision
    {
        Vec3 CollisionBound { get; set; }
        bool OnCollisionEnter();
        bool OnCollisionStay();
        bool OnCollisionLeave();
    }
}