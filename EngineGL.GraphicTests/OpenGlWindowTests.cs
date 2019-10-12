using EngineGL.Window;
using NUnit.Framework;

namespace EngineGL.GraphicTests
{
    [TestFixture]
    public class OpenGlWindowTests
    {
        [Test]
        public void OpenWindow()
        {
            OpenGLWindow window = new OpenGLWindow();
            window.Run();
        }
    }
}