using System;
using System.Collections.Concurrent;
using EngineGL.Core.Attributes;
using EngineGL.Core.Components.Physics;
using Jitter;
using Jitter.Collision;

namespace EngineGL.Impl.Components.Physics
{
    [Experimental]
    public class GlobalPhysicsComponent3D : Component
    {
        private CollisionSystem _collision;
        private World _world;

        private ConcurrentDictionary<Guid, IRigidBody3D> _rigidBodies =
            new ConcurrentDictionary<Guid, IRigidBody3D>();

        public GlobalPhysicsComponent3D()
        {
            _collision = new CollisionSystemSAP();
            _world = new World(_collision);
            _world.Clear();
        }

        public override void OnUpdate(double deltaTime)
        {
            _world.Step((float) deltaTime, true);
        }

        public void AddRigidBody(IRigidBody3D rigidBody)
        {
            _rigidBodies.TryAdd(rigidBody.GameObject.InstanceGuid, rigidBody);
            _world.AddBody(rigidBody.RigidBody);
        }

        public void RemoveRigidBody(IRigidBody3D rigidBody)
        {
            _rigidBodies.TryRemove(rigidBody.GameObject.InstanceGuid, out _);
            _world.RemoveBody(rigidBody.RigidBody);
        }
    }
}