using System;
using EngineGL.GraphicAdapter.Interface;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.GraphicAdapter.Impl.OpenGL2
{
    /// <summary>
    /// OpenGL2 によるIGraphicAdapterの実装
    /// </summary>
    class GraphicAdapter : IGraphicAdapter
    {
        private ISettingHandler _settingHandler = new SettingHandler();

        private VertexHandler _vertexHandler;


        private bool vertexFlag = true;

        public Action<double, IVertexHandler> VertexWriteFunc { set; private get; }
        public Action<double, ISettingHandler> SettingFunc { set; private get; }

        public GraphicAdapter(PrimitiveType primitiveType)
        {
            _vertexHandler = new VertexHandler(primitiveType);
        }

        public void Draw(double deltaTime)
        {
            if (vertexFlag)
            {
                VertexWriteFunc(deltaTime, _vertexHandler);
                vertexFlag = false;
            }
            GL.PushMatrix();
            SettingFunc(deltaTime, _settingHandler);
            _vertexHandler.Draw();
            GL.PopMatrix();
            ErrorCode errorCode = GL.GetError();
            if (errorCode!= ErrorCode.NoError)
                throw new Exception("OpenGL:"+ Enum.GetName(typeof(ErrorCode), errorCode));
        }

        public void Dispose()
        {
            _vertexHandler.Dispose();
        }
    }
}
