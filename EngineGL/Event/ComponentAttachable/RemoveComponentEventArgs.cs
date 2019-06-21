using EngineGL.Core;
using EngineGL.Core.Components;

namespace EngineGL.Event.ComponentAttachable
{
    public class RemoveComponentEventArgs : ComponentAttachableEventArgs, CancelableEvent
    {
        public bool IsCanceled { get; set; }
        public IComponent RemoveComponent { get; }

        public RemoveComponentEventArgs(IComponentAttachable componentAttachable, IComponent removeComponent)
            : base(componentAttachable)
        {
            RemoveComponent = removeComponent;
        }
    }
}