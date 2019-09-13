using System;
using System.Collections.Generic;
using EngineGL.Drawing;
using EngineGL.Mathematics;
using EngineGL.Resource;
using EngineGL.Shape2D;

namespace EngineGL.GraphicEngine
{
    public interface IGraphicAdapter : IDisposable
    {
        void Initialize();

        void BeginDraw();
        void EndDraw();

        Rectangle CreateRectangle(double x, double y, double width, double height);
        Rectangle CreateRectangle((double x, double y) position, (double width, double height) bound);
        Rectangle CreateRectangle(Vector2 position, Vector2 bound);

        Triangle CreateTriangle(double x, double y, double width, double height);
        Triangle CreateTriangle((double x, double y) position, (double width, double height) bound);
        Triangle CreateTriangle(Vector2 position, Vector2 bounds);

        Circle CreateCircle(double x, double y, double radius);
        Circle CreateCircle((double x, double y) position, double radius);
        Circle CreateCircle(Vector2 position, double radius);

        Polygon2D CreatePolygon2D(double x, double y, (double x, double y)[] points);
        Polygon2D CreatePolygon2D((double x, double y) position, (double x, double y)[] points);
        Polygon2D CreatePolygon2D(Vector2 position, Vector2[] points);
        Polygon2D CreatePolygon2D(Vector2 position, IEnumerable<Vector2> points);

        void DrawRectangle(Rectangle rectangle);
        void DrawTriangle(Triangle triangle);
        void DrawCircle(Circle circle);
        void DrawPolygon2D(Polygon2D polygon);

        void SetColor(Color4 color);
        void SetColor(Color3 color);
        void SetUv(Vector2[] uvs);
        void SetUv(IEnumerable<Vector2> uvs);

        //TODO: Normals

        void SetTexture(int textureId);
        void SetTexture(Texture texture);

        Texture CreateTexture();
        Texture CreateTexture(string path);
        Texture CreateTexture(byte[] buf);

        int LoadTexture(Texture texture);
        void UnloadTexture(int textureId);
        void UnloadTexture(Texture texture);
    }
}