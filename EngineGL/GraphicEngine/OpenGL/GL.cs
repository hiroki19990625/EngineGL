using EngineGL.Structs.Drawing;
using EngineGL.Structs.Math;

namespace EngineGL.GraphicEngine
{
    public class GL : IGraphicAdapter
    {
        public bool DrawRect(Vec3 position, Vec3 rotate, Vec3 bounds)
        {
            throw new System.NotImplementedException();
        }

        public bool DrawTriangle(Vec3 pos1, Vec3 pos2, Vec3 pos3, Vec3 rotate)
        {
            throw new System.NotImplementedException();
        }

        public bool DrawCircle(Vec3 position, Vec3 rotate, double radius)
        {
            throw new System.NotImplementedException();
        }

        public bool DrawPolygon(Vec3 position, Vec3 rotate, params Vec3[] vertexPos)
        {
            throw new System.NotImplementedException();
        }

        public bool DrawLine(Vec3 pos1, Vec3 pos2, Vec3 rotate)
        {
            throw new System.NotImplementedException();
        }

        public bool DrawBox(Vec3 position, Vec3 rotate, Vec3 bounds)
        {
            throw new System.NotImplementedException();
        }

        public bool DrawSphere(Vec3 position, Vec3 rotate, double radius)
        {
            throw new System.NotImplementedException();
        }

        public bool DrawPolygon3D(Vec3 position, Vec3 rotate, params Vec3[] vertexPos)
        {
            throw new System.NotImplementedException();
        }

        public bool DrawCamera(Vec3 position, Vec3 rotate)
        {
            throw new System.NotImplementedException();
        }

        public bool DrawPointLight(Vec3 position, double strength)
        {
            throw new System.NotImplementedException();
        }

        public bool DrawAreaLight(Vec3 position, Vec3 bounds)
        {
            throw new System.NotImplementedException();
        }

        public bool DrawDirectionLight(Vec3 position, Vec3 rotate, double strength)
        {
            throw new System.NotImplementedException();
        }

        public bool SetColor(Colour4 color)
        {
            throw new System.NotImplementedException();
        }

        public Colour4 GetColor()
        {
            throw new System.NotImplementedException();
        }

        public bool SetTexture(int textureId)
        {
            throw new System.NotImplementedException();
        }

        public int GetTexture()
        {
            throw new System.NotImplementedException();
        }
    }
}