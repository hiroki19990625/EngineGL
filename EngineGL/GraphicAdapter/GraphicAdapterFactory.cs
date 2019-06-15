using OpenTK.Graphics.OpenGL;

namespace EngineGL.GraphicAdapter
{

    /// <summary>
    /// 各種のGraphicAdapterを生成するファクトリークラス
    /// </summary>
    public static class GraphicAdapterFactory
    {
        public static class OpenGL1
        {
            public static IGraphicAdapter CreateQuads()
                => new Impl.OpenGL1.GraphicAdapter(PrimitiveType.Quads);
            public static IGraphicAdapter CreateLines()
                => new Impl.OpenGL1.GraphicAdapter(PrimitiveType.Lines);
            public static IGraphicAdapter CreatePolygon()
                => new Impl.OpenGL1.GraphicAdapter(PrimitiveType.Polygon);
            public static IGraphicAdapter CreatePoints()
                => new Impl.OpenGL1.GraphicAdapter(PrimitiveType.Points);
        }
    }
}
