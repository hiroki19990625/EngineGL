using System;
using EngineGL.Core.Components.Physics;
using EngineGL.Serializations.Resulter;
using EngineGL.Structs.Math;
using EngineGL.Utils;
using Jitter.Dynamics;
using Newtonsoft.Json;

namespace EngineGL.Impl.Components.Physics
{
    public class RigidBody3D : Component, IRigidBody3D
    {
        private ICollider _collider;

        [SerializeIgnore, JsonIgnore] public RigidBody RigidBody { get; private set; }

        public override void OnInitialze()
        {
            try
            {
                Result<ICollider> collider = GameObject.GetComponentUnsafe<ICollider>();
                if (!collider.IsSuccess)
                    throw new Exception("Require component not found.");

                _collider = collider.Value;
                RigidBody = new RigidBody(_collider.ColliderShape);
                RigidBody.Position = GameObject.Transform.Position;
            }
            catch
            {
                // ignored
            }
        }

        public override void OnComponentInitialized()
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
            GameObject.Transform.Position = (Vec3) RigidBody.Position + _collider.Offset;
        }
    }
}