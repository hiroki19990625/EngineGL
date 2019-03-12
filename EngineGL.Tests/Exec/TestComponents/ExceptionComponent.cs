using System;
using EngineGL.Impl;
using EngineGL.Structs;
using EngineGL.Structs.Math;

namespace EngineGL.Tests.Exec.TestComponents
{
    public class ExceptionComponent : Component
    {
        public int Time { get; set; }

        public override void OnUpdate(double deltaTime)
        {
            if (Time >= 10000)
            {
                throw new Exception();
            }

            GameObject.Position += Vec3.Right * 0.01f;

            Time++;
        }
    }
}