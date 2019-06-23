using EngineGL.Core;
using EngineGL.Core.Components;

namespace EngineGL.Event.ComponentAttachable
{
    public class AddComponentEventArgs : ComponentAttachableEventArgs, CancelableEvent
    {
        public bool IsCanceled { get; set; }
        public IComponent AddComponent { get; }

        public AddComponentEventArgs(IComponentAttachable componentAttachable, IComponent addComponent)
            : base(componentAttachable)
        {
            AddComponent = addComponent;
        }
    }
}