﻿using EngineGL.GraphicAdapter.Interface;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.GraphicAdapter
{

    /// <summary>
    /// 各種のGraphicAdapterを生成するファクトリークラス
    /// </summary>
    public static class GraphicAdapterFactory
    {
        /// <summary>
        /// 三角形描画に特化したアダプターの生成
        /// </summary>
        /// <returns>生成したアダプター</returns>
        public static IGraphicAdapter CreateTriangles()
            => new Impl.OpenGL2.GraphicAdapter(PrimitiveType.Triangles);
        /// <summary>
        /// 四角形描画に特化したアダプターの生成
        /// </summary>
        /// <returns>生成したアダプター</returns>
        public static IGraphicAdapter CreateQuads()
            => new Impl.OpenGL2.GraphicAdapter(PrimitiveType.Quads);
        /// <summary>
        /// 線描画に特化したアダプターの生成
        /// </summary>
        /// <returns>生成したアダプター</returns>
        public static IGraphicAdapter CreateLines()
            => new Impl.OpenGL2.GraphicAdapter(PrimitiveType.Lines);
        /// <summary>
        /// 連続した線描画に特化したアダプターの生成
        /// </summary>
        /// <returns>生成したアダプター</returns>
        public static IGraphicAdapter CreateLinesStrip()
            => new Impl.OpenGL2.GraphicAdapter(PrimitiveType.LineStrip);
        /// <summary>
        /// ポリゴン描画に特化したアダプターの生成
        /// </summary>
        /// <returns>生成したアダプター</returns>
        public static IGraphicAdapter CreatePolygon()
            => new Impl.OpenGL2.GraphicAdapter(PrimitiveType.Polygon);
        /// <summary>
        /// 点描画に特化したアダプターの生成
        /// </summary>
        /// <returns>生成したアダプター</returns>
        public static IGraphicAdapter CreatePoints()
            => new Impl.OpenGL2.GraphicAdapter(PrimitiveType.Points);
    }
}
