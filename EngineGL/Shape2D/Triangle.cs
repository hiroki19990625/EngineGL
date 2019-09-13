using EngineGL.GraphicEngine;
using EngineGL.Mathematics;

namespace EngineGL.Shape2D
{
    public class Triangle : Shape2D
    {
        private IGraphicAdapter _adapter;

        public Vector3 Bound { get; set; }

        public Triangle(IGraphicAdapter adapter)
        {
            _adapter = adapter;
        }

        public override void Draw()
        {
            //TODO: Support 3d
            _adapter.DrawTriangle(this);
        }
    }
}