using System;
using EngineGL.GraphicEngine.OpenGL;

namespace EngineGL.GraphicEngine
{
    public class GraphicFactory
    {
        public IGraphicAdapter CreateOpenGl2Adapter()
        {
            return new GL2();
        }

        public IGraphicAdapter CreateOpenGl4Adapter()
        {
            return new GL4();
        }

        public IGraphicAdapter CreateDirectXAdapter()
        {
            throw new NotImplementedException();
        }

        public IGraphicAdapter GetDefaultAdapter()
        {
#if GRAPHIC_GL
            return CreateOpenGl2Adapter();
#endif
#if GRAPHIC_GL4
            return CreateOpenGl4Adapter();
#endif
#if GRAPHIC_DX
            return CreateDirectXAdapter();
#endif
        }
    }
}