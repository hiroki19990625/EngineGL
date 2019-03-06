using System;
using EngineGL.Impl;
using EngineGL.Structs;

namespace EngineGL.Tests.Exec.TestComponents
{
    public class ExceptionComponent : Component
    {
        public int Time { get; set; }

        public override void OnUpdate()
        {
            if (Time >= 60)
            {
                throw new Exception();
            }

            GameObject.Position += Vec3.Right * 0.01f;

            Time++;
        }
    }
}