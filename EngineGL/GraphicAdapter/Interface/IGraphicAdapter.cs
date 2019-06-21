using EngineGL.Structs.Drawing;
using System;

namespace EngineGL.GraphicAdapter
{
    /// <summary>
    /// 各種描画系ライブラリと依存を分離するためのアダプタークラス
    /// </summary>
    public interface IGraphicAdapter
    {

        /// <summary>
        /// 頂点書き込みをする関数
        /// </summary>
        Action<double, IVertexHandler> VertexWriteFunc { set; }

        /// <summary>
        /// 頂点書き込み前処理をする関数
        /// </summary>
        Action<double, IPreprocessVertexHandler> PreprocessVertexFunc { set; }

        /// <summary>
        /// 描画処理を実行する関数
        /// </summary>
        /// <param name="deltaTime"></param>
        void Draw(double deltaTime);

        /// <summary>
        /// カラーをセットする
        /// </summary>
        /// <param name="colour4"></param>
        void SetColour4(Colour4 colour4);

    }

}
