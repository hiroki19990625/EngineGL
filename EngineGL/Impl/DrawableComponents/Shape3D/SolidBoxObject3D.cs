using EngineGL.GraphicAdapter;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Structs.Math;
using OpenTK;

namespace EngineGL.Impl.DrawableComponents.Shape3D
{
    public class SolidBoxObject3D : DrawableComponent
    {
        public SolidBoxObject3D() : base(GraphicAdapterFactory.CreateQuads())
        {
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);

            float BX = GameObject.Transform.Bounds.X / 2;
            float BY = GameObject.Transform.Bounds.Y / 2;
            float BZ = GameObject.Transform.Bounds.Z / 2;

            Vec3[] verts = new Vec3[]
            {
                new Vec3(-BX, -BY, -BZ), 
                new Vec3(-BX, BY, -BZ),
                new Vec3(BX, BY, -BZ),
                new Vec3(BX, -BY, -BZ),
                new Vec3(-BX, -BY, BZ),
                new Vec3(-BX, BY, BZ),
                new Vec3(BX, BY, BZ),
                new Vec3(BX, -BY, BZ),
            };
            uint[] inds = new uint[]
            {
                //正面
                0, 1, 2, 3,
                //背面
                4, 5, 6, 7,
                //左側面
                0, 1, 5, 4,
                //右側面
                2, 3, 7, 6,
                //上側面
                1, 2, 6, 5,
                //下側面
                0, 3, 7, 4
            };
            Vec3[] normals = new Vec3[verts.Length];

            // Compute normals for each face
            for (int i = 0; i < inds.Length; i += 3)
            {
                Vector3 v1 = verts[inds[i]];
                Vector3 v2 = verts[inds[i + 1]];
                Vector3 v3 = verts[inds[i + 2]];

                // The normal is the cross product of two sides of the triangle
                normals[inds[i]] += (Vec3) Vector3.Cross(v2 - v1, v3 - v1);
                normals[inds[i + 1]] += (Vec3) Vector3.Cross(v2 - v1, v3 - v1);
                normals[inds[i + 2]] += (Vec3) Vector3.Cross(v2 - v1, v3 - v1);
            }

            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = ((Vector3) normals[i]).Normalized();
            }

            vertexHandler.SetNormals(normals);
            vertexHandler.SetIndices(inds);
            vertexHandler.SetVertces3(verts);
        }
    }
}