﻿using EngineGL.Structs.Math;

namespace EngineGL.Core.Components
{
    /// <summary>
    /// オブジェクトの空間座標と、回転、スケールを実装します。
    /// </summary>
    public interface ITransform : IComponent
    {
        /// <summary>
        /// オブジェクトのグローバル空間座標
        /// </summary>
        Vec3 Position { get; set; }

        /// <summary>
        /// オブジェクトのローカル空間座標
        /// </summary>
        Vec3 LocalPosition { get; set; }

        /// <summary>
        /// オブジェクトのグローバルな回転角度
        /// </summary>
        Vec3 Rotation { get; set; }

        /// <summary>
        /// オブジェクトのローカルな回転角度
        /// </summary>
        Vec3 LocalRotation { get; set; }

        /// <summary>
        /// オブジェクトの境界
        /// </summary>
        Vec3 Bounds { get; set; }

        /// <summary>
        /// オブジェクトのスケール
        /// </summary>
        Vec3 Scale { get; set; }

        /// <summary>
        /// 親オブジェクト
        /// </summary>
        ITransform Parent { get; set; }

        /// <summary>
        /// 子オブジェクトの追加
        /// </summary>
        void AddChild(ITransform transform);
    }
}