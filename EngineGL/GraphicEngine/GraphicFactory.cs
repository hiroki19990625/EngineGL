using System;
using EngineGL.GraphicEngine.OpenGL;

namespace EngineGL.GraphicEngine
{
    public static class GraphicFactory
    {
        public static IGraphicAdapter CreateOpenGl2Adapter()
        {
            return new GL2();
        }

        public static IGraphicAdapter CreateOpenGl4Adapter()
        {
            return new GL4();
        }

        public static IGraphicAdapter CreateOpenGlAdapter()
        {
#if GRAPHIC_GL
            return CreateOpenGl2Adapter();
#endif
#if GRAPHIC_GL4
            return CreateOpenGl4Adapter();
#endif
        }

        public static IGraphicAdapter CreateDirectXAdapter()
        {
            throw new NotImplementedException();
        }

        public static IGraphicAdapter GetDefaultAdapter()
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