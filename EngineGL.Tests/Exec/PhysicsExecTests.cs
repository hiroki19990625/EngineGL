using EngineGL.Core;
using EngineGL.Impl;
using EngineGL.Impl.Components.Physics;
using EngineGL.Impl.DrawableComponents.Shape3D;
using EngineGL.Impl.Objects;
using EngineGL.Structs.Math;
using EngineGL.Tests.Exec.TestComponents;
using NUnit.Framework;
using OpenTK.Graphics;

namespace EngineGL.Tests.Exec
{
    [TestFixture]
    public class PhysicsExecTests
    {
        [Test]
        public void Test()
        {
            Scene scene = new Scene();

            GameObject ground = new GameObject();
            ground.SetPosition(new Vec3(0f, -3f, -3f))
                .SetBounds(new Vec3(10f, 1f, 10f));
            ground.AddComponent(new SolidBoxObject3D
            {
                Colour = Color4.Red
            });
            Vec3 p1 = ground.Transform.Position;
            ground.AddComponent(new BoxCollider
            {
                Offset = new Vec3(-p1.X, -p1.Y, -p1.Z) / 2
            });
            RigidBody3D rig1 = new RigidBody3D();
            ground.AddComponent(rig1);
            rig1.RigidBody.IsStatic = true;
            scene.AddObject(ground);

            GameObject player = new GameObject();
            player.SetPosition(new Vec3(0, 0, 0))
                .SetBounds(Vec3.One);
            player.AddComponent(new SolidBoxObject3D
            {
                Colour = Color4.Green
            });
            Vec3 p2 = player.Transform.Position;
            player.AddComponent(new BoxCollider
            {
                Offset = new Vec3(-p2.X, -p2.Y, -p2.Z) / 2
            });
            RigidBody3D rig2 = new RigidBody3D();
            player.AddComponent(rig2);
            player.AddComponent(new PlayerComponent());
            scene.AddObject(player);

            GameObject camera = new GameObject();
            camera.SetPosition(new Vec3(-5, 0, -10));
            camera.AddComponent(new StaticCamera());
            scene.AddObject(camera);

            GameObject g1 = new GameObject();
            GlobalPhysicsComponent3D physics = new GlobalPhysicsComponent3D();
            physics.AddRigidBody(rig1);
            physics.AddRigidBody(rig2);
            g1.AddComponent(physics);
            scene.AddObject(g1);

            IGame game = new GameBuilder()
                .SetDefaultEvents()
                .Build();

            game.LoadScene(scene);
            game.Run(60.0f);
        }
    }
}