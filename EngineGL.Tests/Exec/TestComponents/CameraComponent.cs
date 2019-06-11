using EngineGL.Core;
using EngineGL.Impl.Components;
using EngineGL.Impl.Drawable.Shape2D;
using EngineGL.Impl.Objects;
using EngineGL.Structs.Math;
using OpenTK.Graphics;
using OpenTK.Input;

namespace EngineGL.Tests.Exec.TestComponents
{
    public class CameraComponent : Collision2D
    {
        public override void OnUpdate(double deltaTime)
        {
            base.OnUpdate(deltaTime);
            GameObject.Transform.Rotation += new Vec3(0, -10f / 60f, 0) * (float) deltaTime;
        }
    }
}