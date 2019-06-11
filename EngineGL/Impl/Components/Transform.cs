using EngineGL.Core;
using EngineGL.Structs.Math;
using System.Collections.Concurrent;

namespace EngineGL.Impl
{
    /// <summary>
    /// ITransformを実装するクラス
    /// </summary>
    class Transform : Component, ITransform
    {
        private ConcurrentBag<ITransform> childList = new ConcurrentBag<ITransform>();
        public ITransform parent;

        public Vec3 Position { get; set; }
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
