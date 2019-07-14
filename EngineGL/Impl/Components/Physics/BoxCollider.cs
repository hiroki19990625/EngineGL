using EngineGL.Core.Attributes;
using EngineGL.Core.Components;
using EngineGL.Core.Components.Physics;
using EngineGL.Serializations.Resulter;
using EngineGL.Structs.Math;
using Jitter.Collision.Shapes;
using Newtonsoft.Json;

namespace EngineGL.Impl.Components.Physics
{
    [Experimental]
    public class BoxCollider : Component, ICollider
    {
        [SerializeIgnore, JsonIgnore] public Shape ColliderShape { get; private set; }

        public Vec3 Offset { get; set; }

        public override void OnInitialze()
        {
            ITransform t = GameObject.Transform;
            ColliderShape = new BoxShape(t.Bounds);
        }
    }
}