using System;
using EngineGL.Structs.Math;
using EngineGL.Utils;

namespace EngineGL.Core
{
    public interface ICollision : IComponent
    {
        SerializableAction<IGameObject> OnCollisionEnter { set; }
        SerializableAction<IGameObject> OnCollisionStay { set; }
        SerializableAction<IGameObject> OnCollisionLeave { set; }
    }
}