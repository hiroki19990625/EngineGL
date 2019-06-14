using EngineGL.Impl;
using EngineGL.Impl.Components;
using EngineGL.Impl.Drawable.Shape2D;
using EngineGL.Impl.Objects;
using EngineGL.Structs.Math;
using EngineGL.Tests.Exec.TestComponents;
using NUnit.Framework;
using OpenTK.Graphics;

namespace EngineGL.Tests.Exec
{
    [TestFixture]
    class NodeTest
    {
        [Test]
        public void PositionTest()
        {
            Game game = new GameBuilder()
               .SetTitle("Engine Test")
               .SetExceptinDialog(true)
               .SetDebugLogging(true)
               .SetDefaultEvents()
               .Build();

            game.LoadScene(GetPositionScene());

            game.Run(60.0d);
        }
        private Scene GetPositionScene()
        {

            Scene scene = new Scene();

            SolidBoxObject2D box = new SolidBoxObject2D();
            box.BoxColor = Color4.White;
            box
                .SetPosition(new Vec3(0f, 0f, 0f))
                .SetBounds(new Vec3(1f, 1f, 0f));
            box.AddComponent(new PlayerComponent()
            {
                Bounds = new Vec3(1, 1)
            });
            scene.AddObject(box);

            CircleObject2D circle = new CircleObject2D()
            {
                Radius = 0.5f,
                CircleColor = Color4.Red
            };
            circle.AddComponent(new Collision2D
            {
                Bounds = new Vec3(1, 1),
            });
            circle
                .SetPosition(new Vec3(2, 0, 0))
                .SetBounds(new Vec3(1, 1, 0));
           box.AddChild(circle);

            StaticCamera camera = new StaticCamera();
            camera.Transform.Position = new Vec3(0, 0, -10f);
            scene.AddObject(camera);
            return scene;
        }
    }
}
