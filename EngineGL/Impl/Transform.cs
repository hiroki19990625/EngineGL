﻿using EngineGL.Core;
using EngineGL.Structs.Math;

namespace EngineGL.Impl
{
    /// <summary>
    /// ITransformを実装するクラス
    /// </summary>
    class Transform : ITransform
    {
        public Vec3 Position { get; set; }
        public Vec3 Rotation { get; set; }
        public Vec3 Bounds { get; set; }
        public Vec3 Scale { get; set; } = Vec3.One;
    }
}