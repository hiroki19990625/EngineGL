using EngineGL.GraphicEngine;

namespace EngineGL.Shape2D
{
    public class Circle : Shape2D
    {
        private IGraphicAdapter _adapter;

        public double Radius { get; set; }

        public Circle(IGraphicAdapter adapter)
        {
            _adapter = adapter;
        }

        public override void Draw()
        {
            //TODO: Support 3d
            _adapter.DrawCircle(this);
        }
    }
}