using System;
using EngineGL.Core.Components.Physics;
using EngineGL.Utils;
using Jitter.Dynamics;

namespace EngineGL.Impl.Components.Physics
{
    public class RigidBody3D : Component, IRigidBody3D
    {
        private ICollider _collider;
        public RigidBody RigidBody { get; private set; }

        public override void OnInitialze()
        {
            Result<ICollider> collider = GameObject.GetComponentUnsafe<ICollider>();
            if (!collider.IsSuccess)
                throw new Exception("Require component not found.");

            _collider = collider.Value;
            RigidBody = new RigidBody(_collider.ColliderShape);
            RigidBody.Position = GameObject.Transform.Position;
        }

        public override void OnUpdate(double deltaTime)
        {
            GameObject.Transform.Position = RigidBody.Position;
        }
    }
}