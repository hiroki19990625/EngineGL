using System.Text;
using EngineGL.Impl;
using EngineGL.Impl.Drawable;
using EngineGL.Tests.Exec.TestComponents;
using NLog.Config;
using NLog.Targets;
using NUnit.Framework;
using OpenTK;
using OpenTK.Graphics;

namespace EngineGL.Tests.Exec
{
    [TestFixture]
    public class GamrExecTest
    {
        [Test]
        public void ExecGame()
        {
            Game game = new Game();
            //game.WindowState = WindowState.Fullscreen;
            game.Title = "Engine Test";
            game.ExceptionDialog = true;
            game.DebugLogging = true;
            game.LoggingConfiguration = GetLoggingConfiguration();
            game.Load += (sender, args) => game.LoadDefaultFunc();
            game.Resize += (sender, args) => game.AdjustResize();
            game.RenderFrame += (sender, args) => game.DrawDefaultFunc();

            game.LoadScene(GetInitScene());

            game.Run(60.0d);
        }

        private LoggingConfiguration GetLoggingConfiguration()
        {
            LoggingConfiguration configuration = new LoggingConfiguration();
            configuration.AddTarget(new FileTarget("LogFile")
            {
                Layout = "${longdate} ${logger} ${message}${exception:format=ToString}",
                FileName = "${basedir}/logs/Debug.log",
                Encoding = Encoding.UTF8
            });
            configuration.AddRuleForAllLevels("LogFile");

            return configuration;
        }

        private Scene GetInitScene()
        {
            Scene scene = new Scene();
            scene.AddObject(new SolidBoxObject2D
            {
                BoxColor = Color4.White,
                Position = new Vector3(-0.3f, -0.1f, -2f),
                Bounds = new Vector3(0.2f, 0.2f, 0f)
            });
            SolidPolygonObject2D poly = new SolidPolygonObject2D
            {
                PoligonColor = Color4.Gold,
                Position = new Vector3(-0.3f, -0.3f, -2f)
            };
            poly.Points.Add(new Vector3(0, 0.1f, 0));
            poly.Points.Add(new Vector3(0.1f, 0, 0));
            poly.Points.Add(new Vector3(-0.1f, 0, 0));
            scene.AddObject(poly);
            scene.AddObject(new LineObject
            {
                LineColor = Color4.Red,
                Position = new Vector3(-0.1f, -0.1f, -2f),
                Bounds = new Vector3(0.2f, 0.2f, 0f)
            });
            scene.AddObject(new StippleLineObject
            {
                LineColor = Color4.Aqua,
                Position = new Vector3(-0.1f, 0.1f, -2f),
                Bounds = new Vector3(0.2f, -0.2f, 0f),
                Factor = 1,
                Pattern = 0xf00f
            });
            scene.AddObject(new StippleLineObject
            {
                LineColor = Color4.Green,
                Position = new Vector3(0f, 0.1f, -2f),
                Bounds = new Vector3(0f, -0.2f, 0),
                Factor = 1,
                Pattern = 0xf0af
            });
            StaticCamera camera = new StaticCamera();
            camera.AddComponent(new ExceptionComponent());
            scene.AddObject(camera);

            return scene;
        }
    }
}