using System.Collections.Generic;
using EngineGL.Editor.Core.Control.Window;
using Microsoft.CodeAnalysis;

namespace EngineGL.Editor.Impl.Controls.Window
{
    public class SolutionTreeContent : MyDockContent
    {
        private List<Project> _projects = new List<Project>();

        public SolutionTreeContent(IMainWindow hostWindow, Solution solution) : base(hostWindow)
        {
            foreach (ProjectId projectId in solution.GetProjectDependencyGraph().GetTopologicallySortedProjects())
            {
                Project project = solution.GetProject(projectId);
                _projects.Add(project);
            }
        }
    }
}