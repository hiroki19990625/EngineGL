using System.Collections.Generic;
using EngineGL.GraphicEngine;
using EngineGL.Mathematics;

namespace EngineGL.Shape2D
{
    public class Polygon2D : Shape2D
    {
        private IGraphicAdapter _adapter;

        public List<Vector2> Points { get; } = new List<Vector2>();

        public Polygon2D(IGraphicAdapter adapter)
        {
            _adapter = adapter;
        }

        public override void Draw()
        {
            //TODO: Support 3d
            _adapter.DrawPolygon2D(this);
        }
    }
}