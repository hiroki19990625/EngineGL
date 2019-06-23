using System;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Structs.Drawing;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.GraphicAdapter.Impl.OpenGL2
{
    /// <summary>
    /// OpenGL2 によるIGraphicAdapterの実装
    /// </summary>
    class GraphicAdapter : IGraphicAdapter
    {
        private IPreprocessVertexHandler _preprocessVertexHandler = new PreprocessVertexHandler();

        private VertexHandler _vertexHandler;


        private bool vertexFlag = true;

        public Action<double, IVertexHandler> VertexWriteFunc { set; private get; }
        public Action<double, IPreprocessVertexHandler> PreprocessVertexFunc { set; private get; }

        public GraphicAdapter(PrimitiveType primitiveType, int groupCount)
        {
            _vertexHandler = new VertexHandler(primitiveType, groupCount);
        }

        public void Draw(double deltaTime)
        {
            if (vertexFlag)
            {
                VertexWriteFunc(deltaTime, _vertexHandler);
                vertexFlag = false;
            }
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            PreprocessVertexFunc(deltaTime, _preprocessVertexHandler);
            _vertexHandler.Draw();
            GL.PopMatrix();
        }

        public void SetColour4(Colour4 colour4)
        {
            _vertexHandler.SetColour4(colour4);
        }
    }
}
