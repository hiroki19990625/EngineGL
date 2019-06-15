using System;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.GraphicAdapter.Impl.OpenGL1
{
    /// <summary>
    /// OpenGL1 によるIGraphicAdapterの実装
    /// </summary>
    class GraphicAdapter : IGraphicAdapter
    {
        private IPreprocessVertexHandler _preprocessVertexHandler = new PreprocessVertexHandler();

        private IVertexHandler _vertexHandler = new VertexHandler();

        private PrimitiveType _primitiveType;

        public Action<double, IVertexHandler> VertexWriteFunc { set; private get; }
        public Action<double, IPreprocessVertexHandler> PreprocessVertexFunc { set; private get; }

        public GraphicAdapter(PrimitiveType primitiveType)
        {
            _primitiveType = primitiveType;
        }

        public void Draw(double deltaTime)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            PreprocessVertexFunc(deltaTime,_preprocessVertexHandler);
            GL.Begin(_primitiveType);
            VertexWriteFunc(deltaTime, _vertexHandler);
            GL.End();
            GL.PopMatrix();
        }
    }
}
