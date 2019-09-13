using EngineGL.GraphicEngine;
using EngineGL.Mathematics;

namespace EngineGL.Shape2D
{
    public class Rectangle : Shape2D
    {
        private IGraphicAdapter _adapter;

        public Vector3 Bound { get; set; }

        public Rectangle(IGraphicAdapter adapter)
        {
            _adapter = adapter;
        }

        public override void Draw()
        {
            //TODO: Support 3d
            _adapter.DrawRectangle(this);
        }
    }
}