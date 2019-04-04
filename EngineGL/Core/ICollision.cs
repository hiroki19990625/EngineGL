using System;
using EngineGL.Structs.Math;
using EngineGL.Utils;

namespace EngineGL.Core
{
    public interface ICollision : IComponent
    {
        void OnCollisionEnter(IGameObject gameObject);
        void OnCollisionStay(IGameObject gameObject);
        void OnCollisionLeave(IGameObject gameObject);
    }
}