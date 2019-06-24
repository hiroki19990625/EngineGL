using EngineGL.Core;
using EngineGL.Core.Components;

namespace EngineGL.Event.ComponentAttachable
{
    public class RemoveComponentEventArgs : ComponentAttachableEventArgs
    {
        public IComponent RemoveComponent { get; }

        public RemoveComponentEventArgs(IComponentAttachable componentAttachable, IComponent removeComponent)
            : base(componentAttachable)
        {
            RemoveComponent = removeComponent;
        }
    }
}