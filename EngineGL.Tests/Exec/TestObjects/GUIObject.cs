using System;
using EngineGL.Impl;
using ImGuiNET;

namespace EngineGL.Tests.Exec.TestObjects
{
    public class GUIObject : GUIRender
    {
        private string _inputStr = "あ";

        public override void OnGUI(double deltaTime)
        {
            base.OnGUI(deltaTime);

            ImGui.Begin("Debug Window");
            {
                ImGui.InputText("input", ref _inputStr, 10);
                ImGui.InputText("input2", ref _inputStr, 10);
                if (ImGui.Button("Clear"))
                {
                    _inputStr = "";
                }
            }
            ImGui.End();
        }
    }
}