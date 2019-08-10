using System;
using EngineGL.Core.Attributes;
using EngineGL.Core.Components.Physics;
using EngineGL.Serializations.Resulter;
using EngineGL.Structs.Math;
using EngineGL.Utils;
using Jitter.Dynamics;
using Jitter.LinearMath;
using Newtonsoft.Json;
using OpenTK;
using Matrix3 = OpenTK.Matrix3;

namespace EngineGL.Impl.Components.Physics
{
    [Experimental]
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
                RigidBody.Position = GameObject.Transform.Position + GameObject.Transform.Bounds / 2;
                RigidBody.Material.Restitution = 0.0f;
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
            RigidBody.Position = GameObject.Transform.LocalPosition + GameObject.Transform.Bounds / 2;
        }

        public override void OnUpdate(double deltaTime)
        {
            JMatrix m = RigidBody.Orientation;
            GameObject.Transform.LocalPosition = (Vec3) RigidBody.Position - GameObject.Transform.Bounds / 2;
            GameObject.Transform.LocalRotation = Quaternion
                .FromMatrix(new Matrix3(m.M11, m.M12, m.M13, m.M21, m.M22, m.M23, m.M31, m.M32, m.M33)).Xyz;
        }
    }
}