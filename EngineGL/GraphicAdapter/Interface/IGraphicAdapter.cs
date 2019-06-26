using System;

namespace EngineGL.GraphicAdapter.Interface
{
    /// <summary>
    /// 各種描画系ライブラリと依存を分離するためのアダプタークラス
    /// </summary>
    public interface IGraphicAdapter : IDisposable
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
    }

}
