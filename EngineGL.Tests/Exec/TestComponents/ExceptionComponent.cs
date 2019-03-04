using System;
using EngineGL.Impl;
using OpenTK;

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
            
            ((GameObject)ParentObject).Position += Vector3.UnitX * 0.01f;

            Time++;
        }
    }
}