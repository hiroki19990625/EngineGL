using System.Collections.Generic;
using EngineGL.Structs.Drawing;
using EngineGL.Structs.Math;


namespace EngineGL.GraphicAdapter
{
    public interface IVertexHandler
    {
        void Vertces3(IEnumerable<Vec3> vecs);
        void Vertces2(IEnumerable<Vec2> vecs);
        void Indices(IEnumerable<uint> indices);
    }
}
