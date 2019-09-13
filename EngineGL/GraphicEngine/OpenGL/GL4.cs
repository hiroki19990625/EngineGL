using System.Collections.Generic;
using EngineGL.Drawing;
using EngineGL.Mathematics;
using EngineGL.Resource;
using EngineGL.Shape2D;

namespace EngineGL.GraphicEngine.OpenGL
{
    public class GL4 : IGraphicAdapter
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public void BeginDraw()
        {
            throw new System.NotImplementedException();
        }

        public void EndDraw()
        {
            throw new System.NotImplementedException();
        }

        public Rectangle CreateRectangle(double x, double y, double width, double height)
        {
            throw new System.NotImplementedException();
        }

        public Rectangle CreateRectangle((double x, double y) position, (double width, double height) bound)
        {
            throw new System.NotImplementedException();
        }

        public Rectangle CreateRectangle(Vector2 position, Vector2 bound)
        {
            throw new System.NotImplementedException();
        }

        public Triangle CreateTriangle(double x, double y, double width, double height)
        {
            throw new System.NotImplementedException();
        }

        public Triangle CreateTriangle((double x, double y) position, (double width, double height) bound)
        {
            throw new System.NotImplementedException();
        }

        public Triangle CreateTriangle(Vector2 position, Vector2 bounds)
        {
            throw new System.NotImplementedException();
        }

        public Circle CreateCircle(double x, double y, double radius)
        {
            throw new System.NotImplementedException();
        }

        public Circle CreateCircle((double x, double y) position, double radius)
        {
            throw new System.NotImplementedException();
        }

        public Circle CreateCircle(Vector2 position, double radius)
        {
            throw new System.NotImplementedException();
        }

        public Polygon2D CreatePolygon2D(double x, double y, (double x, double y)[] points)
        {
            throw new System.NotImplementedException();
        }

        public Polygon2D CreatePolygon2D((double x, double y) position, (double x, double y)[] points)
        {
            throw new System.NotImplementedException();
        }

        public Polygon2D CreatePolygon2D(Vector2 position, Vector2[] points)
        {
            throw new System.NotImplementedException();
        }

        public Polygon2D CreatePolygon2D(Vector2 position, IEnumerable<Vector2> points)
        {
            throw new System.NotImplementedException();
        }

        public void DrawRectangle(Rectangle rectangle)
        {
            throw new System.NotImplementedException();
        }

        public void DrawTriangle(Triangle triangle)
        {
            throw new System.NotImplementedException();
        }

        public void DrawCircle(Circle circle)
        {
            throw new System.NotImplementedException();
        }

        public void DrawPolygon2D(Polygon2D polygon)
        {
            throw new System.NotImplementedException();
        }

        public void SetColor(Color4 color)
        {
            throw new System.NotImplementedException();
        }

        public void SetColor(Color3 color)
        {
            throw new System.NotImplementedException();
        }

        public void SetUv(Vector2[] uvs)
        {
            throw new System.NotImplementedException();
        }

        public void SetUv(IEnumerable<Vector2> uvs)
        {
            throw new System.NotImplementedException();
        }

        public void SetTexture(int textureId)
        {
            throw new System.NotImplementedException();
        }

        public void SetTexture(Texture texture)
        {
            throw new System.NotImplementedException();
        }

        public Texture CreateTexture()
        {
            throw new System.NotImplementedException();
        }

        public Texture CreateTexture(string path)
        {
            throw new System.NotImplementedException();
        }

        public Texture CreateTexture(byte[] buf)
        {
            throw new System.NotImplementedException();
        }

        public int LoadTexture(Texture texture)
        {
            throw new System.NotImplementedException();
        }

        public void UnloadTexture(int textureId)
        {
            throw new System.NotImplementedException();
        }

        public void UnloadTexture(Texture texture)
        {
            throw new System.NotImplementedException();
        }
    }
}