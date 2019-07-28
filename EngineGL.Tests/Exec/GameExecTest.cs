using System;
using System.Drawing;
using System.IO;
using System.Text;
using EngineGL.Core;
using EngineGL.Core.Resource;
using EngineGL.Impl;
using EngineGL.Impl.Components.Physics;
using EngineGL.Impl.DrawableComponents;
using EngineGL.Impl.DrawableComponents.Shape2D;
using EngineGL.Impl.DrawableComponents.Shape3D;
using EngineGL.Impl.Objects;
using EngineGL.Impl.Resource;
using EngineGL.Structs.Math;
using EngineGL.Tests.Exec.TestComponents;
using NLog.Config;
using NLog.Targets;
using NUnit.Framework;
using OpenTK.Graphics;

namespace EngineGL.Tests.Exec
{
    [TestFixture]
    public class GameExecTest
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var dir = Path.GetDirectoryName(typeof(GameExecTest).Assembly.Location);
            Environment.CurrentDirectory = dir;
        }

        [Test, Order(1)]
        public void ExecGame()
        {
            IGame game = new GameBuilder()
                .SetTitle("Engine Test")
                .SetExceptinDialog(true)
                .SetDebugLogging(true)
                .SetLoggingConfiguration(GetLoggingConfiguration())
                .SetDefaultEvents()
                .Build();

            game.LoadScene(GetInitScene());
            game.LoadScene(new Scene());

            game.Run(60.0d);
        }

        [Test, Order(2)]
        public void ExecGame2()
        {
            IGame game = new GameBuilder()
                .SetTitle("Engine Test")
                .SetExceptinDialog(true)
                .SetDebugLogging(true)
                .SetLoggingConfiguration(GetLoggingConfiguration())
                .SetDefaultEvents()
                .Build();

            Guid hash = game.PreLoadScene<Scene>("scene.json").Value;
            game.LoadScene(hash).Value.Save("scene.json");

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

            GameObject g1 = new GameObject();
            g1.SetPosition(new Vec3(0f, 0f, 0f))
                .SetBounds(new Vec3(1f, 1f, 0f));

            g1.AddComponent(new SolidBoxObject2D
            {
                Colour = Color4.White
            });
            g1.AddComponent(new RotateComponent());
            scene.AddObject(g1);

            GameObject g2 = new GameObject();
            g2.SetPosition(new Vec3(-2f, -1f, 0f))
                .SetBounds(new Vec3(2f, 2f, 2f));
            g2.AddComponent(new SolidBoxObject3D
            {
                Colour = Color4.Pink,
                Layer = 99
            });
            //g2.AddComponent(new RotateComponent());
            g2.AddComponent(new BoxCollider());
            RigidBody3D body3D = new RigidBody3D();
            g2.AddComponent(body3D);
            scene.AddObject(g2);

            GameObject plane = new GameObject();
            plane.SetPosition(new Vec3(-5f, -4f, 0f))
                .SetBounds(new Vec3(8f, 2f, 8f));
            plane.AddComponent(new SolidBoxObject3D
            {
                Colour = Color4.Green,
                Layer = 99
            });
            plane.AddComponent(new BoxCollider());
            RigidBody3D body3D1 = new RigidBody3D();
            plane.AddComponent(body3D1);
            body3D1.RigidBody.IsStatic = true;
            scene.AddObject(plane);

            GameObject g3 = new GameObject();
            SolidPolygonObject2D poly = new SolidPolygonObject2D
            {
                Colour = Color4.Gold,
            };
            g3.SetPosition(new Vec3(-3f, -3f, 0f));
            poly.Layer = 1;
            poly.Points.Add(new Vec3(1f, 1.5f, 0));
            poly.Points.Add(new Vec3(0f, 0, 0));
            poly.Points.Add(new Vec3(2f, 0, 0));
            g3.AddComponent(poly);
            g3.AddComponent(new RotateComponent());
            scene.AddObject(g3);

            GameObject g4 = new GameObject();
            SolidPolygonObject3D poly3 = new SolidPolygonObject3D
            {
                Colour = Color4.Gray,
            };
            g4.SetPosition(new Vec3(1f, 1f, 0f))
                .SetBounds(new Vec3(0, 0, 3f));
            poly3.Layer = 1;
            poly3.Points.Add(new Vec3(0, 0));
            poly3.Points.Add(new Vec3(1f, 0));
            poly3.Points.Add(new Vec3(1.5f, 0.86f));
            poly3.Points.Add(new Vec3(1.0f, 1.72f));
            poly3.Points.Add(new Vec3(0, 1.72f));
            poly3.Points.Add(new Vec3(-0.5f, 0.87f));
            g4.AddComponent(poly3);
            g4.AddComponent(new RotateComponent());
            scene.AddObject(g4);

            GameObject g5 = new GameObject();
            g5.SetPosition(new Vec3(2, 2, 0));
            g5.AddComponent(new PointsObject2D
                {
                    Colour = Color4.White,
                    PointSize = 10
                }
            );
            scene.AddObject(g5);

            GameObject g6 = new GameObject();
            g6.AddComponent(new CircleObject2D
            {
                Radius = 0.5f,
                Colour = Color4.Red
            });
            g6.SetPosition(new Vec3(3.5f, 1f, 0))
                .SetBounds(new Vec3(1, 1, 0));
            scene.AddObject(g6);

            GameObject g7 = new GameObject();
            g7.SetBounds(new Vec3(3, 3, 0))
                .SetPosition(new Vec3(-2, -2, 0));
            g7.AddComponent(new RawTexture2D("Images/download.png"));
            scene.AddObject(g7);

            GameObject g8 = new GameObject();
            g8.SetBounds(new Vec3(1, 1, 0))
                .SetPosition(new Vec3(0.5f, 0, 0));
            g8.AddComponent(new TextRenderer(100, 100)
            {
                FontColor = Color.Red,
                Text = "Hello"
            });
            scene.AddObject(g8);

            IAudio audio = ResourceManager.LoadWave("Sounds/Mixdown2.wav");
            audio.SetLoop(true);
            //audio.Play();

            GameObject physics = new GameObject();
            GlobalPhysicsComponent3D physicsComponent3D = new GlobalPhysicsComponent3D();
            physicsComponent3D.AddRigidBody(g2.GetComponentUnsafe<RigidBody3D>().Value);
            physicsComponent3D.AddRigidBody(plane.GetComponentUnsafe<RigidBody3D>().Value);
            physics.AddComponent(physicsComponent3D);
            scene.AddObject(physics);

            GameObject camera = new GameObject();
            camera.SetPosition(new Vec3(0, 0, -10f));
            camera.AddComponent(new StaticCamera());
            scene.AddObject(camera);
            scene.Save("scene.json");

            return scene;
        }
    }
}