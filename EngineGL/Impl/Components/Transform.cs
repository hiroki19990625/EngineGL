using System;
using System.Collections.Concurrent;
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

        public Vec3 Position
        {
            get
            {
                if (parent == null) return LocalPosition;
                return parent.Position + EulerMatrix3 * LocalPosition;
            }
            set
            {
                if (parent == null) LocalPosition = value;
                else LocalPosition = value - parent.Position;
            }
        }

        public Vec3 LocalPosition { get; set; }

        public Vec3 Rotation
        {
            get
            {
                if (parent == null) return LocalRotation;
                return parent.Rotation + LocalRotation;
            }
            set
            {
                if (parent == null) LocalRotation = value;
                else LocalRotation =value - parent.Rotation;
            }
        }

        public Vec3 LocalRotation { get; set; }
        public Vec3 Bounds { get; set; }
        public Vec3 Scale { get; set; } = Vec3.One;
        public ITransform Parent { get => parent; set => parent = value; }

        public void AddChild(ITransform transform)
        {
            childList.Add(transform);
            transform.Parent = this;
        }

        private Matrix3 EulerMatrix3
        {
            get
            {
                float xcos = (float)Math.Cos(parent.Rotation.X / 180 * Math.PI);
                float xsin = (float)Math.Sin(parent.Rotation.X / 180 * Math.PI);
                float ycos = (float)Math.Cos(parent.Rotation.Y / 180 * Math.PI);
                float ysin = (float)Math.Sin(parent.Rotation.Y / 180 * Math.PI);
                float zcos = (float)Math.Cos(parent.Rotation.Z / 180 * Math.PI);
                float zsin = (float)Math.Sin(parent.Rotation.Z / 180 * Math.PI);
                return
                    new Matrix3(new Vec3(ycos, 0, -ysin), new Vec3(0, 1, 0), new Vec3(ysin, 0, ycos))
                    * new Matrix3(new Vec3(zcos, zsin, 0), new Vec3(-zsin, zcos, 0), new Vec3(0, 0, 1))
                    * new Matrix3(new Vec3(1, 0, 0), new Vec3(0, xcos, xsin), new Vec3(0, -xsin, xcos));
            }
        }
    }
}
