using EngineGL.Core;

namespace EngineGL.Event.Scene
{
    public class RemoveObjectEventArgs : SceneEventArgs, CancelableEvent
    {
        public bool IsCanceled { get; set; }
        public IObject RemoveObject { get; set; }

        public RemoveObjectEventArgs(IScene scene, IObject removeObject) : base(scene)
        {
            RemoveObject = removeObject;
        }
    }
}