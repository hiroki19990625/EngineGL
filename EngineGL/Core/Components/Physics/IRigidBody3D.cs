using Jitter.Dynamics;

namespace EngineGL.Core.Components.Physics
{
    public interface IRigidBody3D : IComponent
    {
        RigidBody RigidBody { get; }
    }
}