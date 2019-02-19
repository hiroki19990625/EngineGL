using EngineGL.Core;

namespace EngineGL.Event.Scene
{
    public class AddObjectEventArgs : SceneEventArgs, CancelableEvent
    {
        public bool IsCanceled { get; set; }
        public IObject AddObject { get; set; }

        public AddObjectEventArgs(IScene scene, IObject addObject) : base(scene)
        {
            AddObject = addObject;
        }
    }
}