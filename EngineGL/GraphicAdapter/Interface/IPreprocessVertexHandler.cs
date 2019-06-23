using EngineGL.Core.Resource;
using EngineGL.Structs.Drawing;
using EngineGL.Structs.Math;

namespace EngineGL.GraphicAdapter.Interface
{
    public interface IPreprocessVertexHandler
    {
        /// <summary>
        /// 行列に加算して平行移動する
        /// </summary>
        /// <param name="vec">加算するベクトル</param>

        void Translate(Vec3 vec);

        /// <summary>
        /// ある軸を元にして回転をする
        /// </summary>
        /// <param name="angle">回転角度（度数法）</param>
        /// <param name="x">回転軸のX成分</param>
        /// <param name="y">回転軸のY成分</param>
        /// <param name="z">回転軸のZ成分</param>
        void Rotate(float angle, float x, float y, float z);

        /// <summary>
        /// オイラー回転する
        /// </summary>
        /// <param name="angles"></param>
        void Euler(Vec3 angles);

        /// <summary>
        /// カラーのセット
        /// </summary>
        /// <param name="colour4"></param>
        void SetColour(Colour4 colour4);

        /// <summary>
        /// テクスチャのセット
        /// </summary>
        /// <param name="texture"></param>
        void SetTexture(ITexture texture);

        /// <summary>
        /// 線の太さをセット
        /// </summary>
        /// <param name="lineWidth"></param>
        void SetLineWidth(float lineWidth);

        /// <summary>
        /// 点の大きさをセット
        /// </summary>
        /// <param name="PointSize"></param>
        void SetPointSize(float PointSize);
    }
}
