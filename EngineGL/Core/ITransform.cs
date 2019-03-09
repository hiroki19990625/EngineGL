using EngineGL.Structs;
using EngineGL.Structs.Math;

namespace EngineGL.Core
{
    public interface ITransform
    {
        Vec3 Position { get; set; }
        Vec3 Rotation { get; set; }
        Vec3 Bounds { get; set; }
        Vec3 Scale { get; set; }
    }
}