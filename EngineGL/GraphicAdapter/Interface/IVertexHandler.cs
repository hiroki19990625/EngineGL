using System.Collections.Generic;
using EngineGL.Core.Resource;
using EngineGL.Structs.Math;


namespace EngineGL.GraphicAdapter
{
    public interface IVertexHandler
    {
        /// <summary>
        /// テクスチャをセットする
        /// </summary>
        /// <param name="texture"></param>
        void SetTexture(ITexture texture);

        /// <summary>
        /// 3D頂点データをセットする
        /// </summary>
        /// <param name="vecs"></param>
        void SetVertces3(IEnumerable<Vec3> vecs);

        /// <summary>
        /// 2D頂点データをセットする
        /// </summary>
        /// <param name="vecs"></param>
        void SetVertces2(IEnumerable<Vec2> vecs);

        /// <summary>
        /// インデックスデータをセットする
        /// </summary>
        /// <param name="indices"></param>
        void SetIndices(IEnumerable<uint> indices);

        /// <summary>
        /// UVデータをセットする
        /// </summary>
        /// <param name="vecs"></param>
        void SetUv(IEnumerable<Vec2> vecs);
    }
}
