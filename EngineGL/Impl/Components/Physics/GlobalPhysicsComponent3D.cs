using EngineGL.Core.Components.Physics;
using Jitter;
using Jitter.Collision;

namespace EngineGL.Impl.Components.Physics
{
    public class GlobalPhysicsComponent3D : Component
    {
        private CollisionSystem _collision;
        private World _world;

        public GlobalPhysicsComponent3D()
        {
            _collision = new CollisionSystemSAP();
            _world = new World(_collision);
        }

        public override void OnUpdate(double deltaTime)
        {
            _world.Step((float) deltaTime, true);
        }

        public void AddRigidBody(IRigidBody3D rigidBody)
        {
            _world.AddBody(rigidBody.RigidBody);
        }

        public void RemoveRigidBody(IRigidBody3D rigidBody)
        {
            _world.RemoveBody(rigidBody.RigidBody);
        }
    }
}