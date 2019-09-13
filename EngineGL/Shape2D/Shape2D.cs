using EngineGL.Mathematics;

namespace EngineGL.Shape2D
{
    public abstract class Shape2D : IShape2D
    {
        public Vector3 Position { get; set; }

        public abstract void Draw();
    }
}