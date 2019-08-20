using EngineGL.Structs.Drawing;
using EngineGL.Structs.Math;

namespace EngineGL.GraphicEngine
{
    public interface IGraphicAdapter
    {
        bool DrawRect(Vec3 position, Vec3 rotate, Vec3 bounds);
        bool DrawTriangle(Vec3 pos1, Vec3 pos2, Vec3 pos3, Vec3 rotate);
        bool DrawCircle(Vec3 position, Vec3 rotate, double radius);
        bool DrawPolygon(Vec3 position, Vec3 rotate, params Vec3[] vertexPos);

        bool DrawLine(Vec3 pos1, Vec3 pos2, Vec3 rotate);

        bool DrawBox(Vec3 position, Vec3 rotate, Vec3 bounds);
        bool DrawSphere(Vec3 position, Vec3 rotate, double radius);
        bool DrawPolygon3D(Vec3 position, Vec3 rotate, params Vec3[] vertexPos);

        bool DrawCamera(Vec3 position, Vec3 rotate);

        bool DrawPointLight(Vec3 position, double strength);
        bool DrawAreaLight(Vec3 position, Vec3 bounds);
        bool DrawDirectionLight(Vec3 position, Vec3 rotate, double strength);

        bool SetColor(Colour4 color);
        Colour4 GetColor();

        bool SetTexture(int textureId);
        int GetTexture();
    }
}