using EngineGL.Core.Attributes;
using Jitter.Dynamics;

namespace EngineGL.Core.Components.Physics
{
    [Experimental]
    public interface IRigidBody3D : IComponent
    {
        RigidBody RigidBody { get; }
    }
}