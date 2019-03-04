using System;
using EngineGL.Impl;
using EngineGL.Impl.Drawable;
using NUnit.Framework;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

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
            game.Load += Game_OnLoad;
            game.Resize += (sender, args) =>
            {
                GL.Viewport(game.ClientRectangle);
                GL.MatrixMode(MatrixMode.Projection);
                Matrix4 projection =
                    Matrix4.CreatePerspectiveFieldOfView((float) Math.PI / 4, (float) game.Width / (float) game.Height,
                        1.0f,
                        64.0f);
                GL.LoadMatrix(ref projection);
            };
            game.RenderFrame += Game_OnRenderFrame;

            Scene scene = new Scene();
            scene.AddObject(new LineObject
            {
                LineColor = Color4.Red,
                Position = new Vector3(-0.1f, -0.1f, -2f),
                Bounds = new Vector3(0.1f, 0.1f, -2f)
            });
            scene.AddObject(new StippleLineObject
            {
                LineColor = Color4.Aqua,
                Position = new Vector3(-0.1f, 0.1f, -2f),
                Bounds = new Vector3(0.1f, -0.1f, -2f),
                Factor = 1,
                Pattern = 0xf00f
            });
            scene.AddObject(new StippleLineObject
            {
                LineColor = Color4.Green,
                Position = new Vector3(0f, 0.1f, -2f),
                Bounds = new Vector3(0f, -0.1f, -2f),
                Factor = 1,
                Pattern = 0xf0af
            });
            scene.AddObject(new StaticCamera());
            game.LoadScene(scene);

            game.Run(60.0d);
        }

        private void Game_OnRenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        private void Game_OnLoad(object sender, EventArgs e)
        {
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}