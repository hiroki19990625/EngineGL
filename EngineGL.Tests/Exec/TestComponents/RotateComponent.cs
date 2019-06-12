using EngineGL.Impl;
using EngineGL.Structs.Math;

namespace EngineGL.Tests.Exec.TestComponents
{
    class RotateComponent : Component
    {
        public override void OnUpdate(double deltaTime)
        {
            base.OnUpdate(deltaTime);
            if(GameObject.Transform.Rotation.X<90)
                GameObject.Transform.Rotation = new Vec3(1, 0, 0) + GameObject.Transform.Rotation;
            else if(GameObject.Transform.Rotation.Y < 90)
                GameObject.Transform.Rotation = new Vec3(0, 1, 0) + GameObject.Transform.Rotation;
            else
                GameObject.Transform.Rotation = new Vec3(0, 0, 1) + GameObject.Transform.Rotation;

        }
    }
}
