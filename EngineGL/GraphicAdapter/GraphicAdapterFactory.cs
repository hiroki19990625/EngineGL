using OpenTK.Graphics.OpenGL;

namespace EngineGL.GraphicAdapter
{

    /// <summary>
    /// 各種のGraphicAdapterを生成するファクトリークラス
    /// </summary>
    public static class GraphicAdapterFactory
    {
        public static class OpenGL2
        {
            public static IGraphicAdapter CreateTriangles()
                => new Impl.OpenGL2.GraphicAdapter(PrimitiveType.Triangles, 3);
            public static IGraphicAdapter CreateLines()
                => new Impl.OpenGL2.GraphicAdapter(PrimitiveType.Lines, 2);
            public static IGraphicAdapter CreateLinesStrip()
                => new Impl.OpenGL2.GraphicAdapter(PrimitiveType.LineStrip, 2);
            public static IGraphicAdapter CreatePolygon()
                => new Impl.OpenGL2.GraphicAdapter(PrimitiveType.Polygon, 3);
            public static IGraphicAdapter CreatePoints()
                => new Impl.OpenGL2.GraphicAdapter(PrimitiveType.Points, 1);
        }
    }
}
