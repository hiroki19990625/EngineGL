using System.Collections.Generic;
using EngineGL.Core.Resource;
using EngineGL.Structs.Drawing;
using EngineGL.Structs.Math;


namespace EngineGL.GraphicAdapter
{
    public interface IVertexHandler
    {
        void SetTexture(ITexture texture);

        void Vertces3(IEnumerable<Vec3> vecs);
        void Vertces2(IEnumerable<Vec2> vecs);
        void Indices(IEnumerable<uint> indices);

        void Uv(IEnumerable<Vec2> vecs);
    }
}
