using System.Collections.Concurrent;
using EngineGL.Core;
using EngineGL.Core.Components;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.Components
{
    /// <summary>
    /// ITransformを実装するクラス
    /// </summary>
    class Transform : Component, ITransform
    {
        private ConcurrentBag<ITransform> childList = new ConcurrentBag<ITransform>();
        public ITransform parent;

        public Vec3 Position {
            get {
                if (parent == null) return LocalPosition;
                return parent.Position + LocalPosition;
            }
            set {
                if (parent == null) LocalPosition = value;
                else LocalPosition = value - parent.Position;
            }
        }
        public Vec3 LocalPosition { get; set; }
        public Vec3 Rotation { get; set; }
        public Vec3 Bounds { get; set; }
        public Vec3 Scale { get; set; } = Vec3.One;
        public ITransform Parent { get => parent; set => parent = value; }

        public void AddChild(ITransform transform)
        {
            childList.Add(transform);
            transform.Parent = this;
        }
    }
}
