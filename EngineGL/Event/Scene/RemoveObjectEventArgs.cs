using EngineGL.Core;

namespace EngineGL.Event.Scene
{
    public class RemoveObjectEventArgs : SceneEventArgs
    {
        public IObject RemoveObject { get; }

        public RemoveObjectEventArgs(IScene scene, IObject removeObject) : base(scene)
        {
            RemoveObject = removeObject;
        }
    }
}