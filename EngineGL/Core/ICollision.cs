using System;

namespace EngineGL.Core
{
    public interface ICollision
    {
        bool OnCollisionEnter();
        bool OnCollisionStay();
        bool OnCollisionLeave();
    }
}