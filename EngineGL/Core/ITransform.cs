using System;
using OpenTK;

namespace EngineGL.Core
{
    public interface ITransform
    {
        Vector3 Position { get; set; }
        Vector3 Rotation { get; set; }
        Vector3 Bounds { get; set; }
        Vector3 Scale { get; set; }
    }
}