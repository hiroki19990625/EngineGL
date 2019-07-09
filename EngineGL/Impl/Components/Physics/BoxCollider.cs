using EngineGL.Core.Components;
using EngineGL.Core.Components.Physics;
using EngineGL.Structs.Math;
using Jitter.Collision.Shapes;
using Jitter.LinearMath;

namespace EngineGL.Impl.Components.Physics
{
    public class BoxCollider : Component, ICollider
    {
        public Shape ColliderShape { get; private set; }

        public Vec3 Offset { get; set; }

        public override void OnInitialze()
        {
            ITransform t = GameObject.Transform;
            ColliderShape = new BoxShape(t.Bounds);
        }

        public override void OnUpdate(double deltaTime)
        {
            JBBox box = ColliderShape.BoundingBox;
            box.Min = Offset;
            box.Max = GameObject.Transform.Bounds;
        }
    }
}