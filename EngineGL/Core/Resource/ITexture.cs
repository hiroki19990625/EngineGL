using System;

namespace EngineGL.Core.Resource
{
    /// <summary>
    /// 表示可能なテクスチャを実装します。
    /// </summary>
    public interface ITexture : IDisposable
    {
        /// <summary>
        /// テクスチャのファイル名
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// テクスチャのメモリハッシュ。
        /// </summary>
        int TextureHash { get; }
    }
}