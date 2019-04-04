using EngineGL.Impl.Objects;
using ImGuiNET;

namespace EngineGL.Tests.Exec.TestObjects
{
    public class CollisionInspector : GUIRender
    {
        public bool Collision { get; set; }

        public override void OnGUI(double deltaTime)
        {
            base.OnGUI(deltaTime);

            ImGui.Begin("Collision Inspector");
            {
                bool prop1 = Collision;
                ImGui.Checkbox("collision", ref prop1);
                Collision = prop1;
            }
            ImGui.End();
        }

        public void SetCollision(bool value)
        {
            Collision = value;
        }
    }
}