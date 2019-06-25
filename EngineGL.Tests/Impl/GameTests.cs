using EngineGL.Core;
using EngineGL.Impl;
using NUnit.Framework;

namespace EngineGL.Tests.Impl
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void SceneTest()
        {
            IGame game = new GameBuilder().Build();
            Scene sce = new Scene();

            int count = 0;
            game.SceneEvents.UnloadSceneEvent += (sender, args) =>
            {
                if (args.UnloadScene.GetHashCode() == sce.GetHashCode())
                {
                    count++;
                }
            };
            // ロードに成功
            Assert.True(game.LoadScene(sce).IsSuccess);
            // ロードに成功しているので取得に成功
            Assert.True(game.GetScene(sce.GetHashCode()).IsSuccess);

            Scene scene = new Scene();
            Scene scene2 = new Scene();
            Assert.True(game.LoadScene(scene).IsSuccess);
            // 既に追加されているので失敗
            Assert.False(game.LoadScene(scene).IsSuccess);
            // ロードに成功しているので取得に成功
            Assert.True(game.GetScene(scene.GetHashCode()).IsSuccess);
            Assert.True(game.UnloadScene(scene).IsSuccess);
            // 既にアンロードされているので失敗
            Assert.False(game.UnloadScene(scene).IsSuccess);
            // アンロードされているので取得に失敗
            Assert.False(game.GetScene(scene.GetHashCode()).IsSuccess);
            Assert.True(game.LoadScene(scene).IsSuccess);
            Assert.True(game.GetScene(scene.GetHashCode()).IsSuccess);
            Assert.True(game.LoadScene(scene2).IsSuccess);
            Assert.True(game.GetScene(scene2.GetHashCode()).IsSuccess);
            Assert.True(game.UnloadScenes());
            // 全てアンロードしたので失敗
            Assert.False(game.GetScene(scene.GetHashCode()).IsSuccess);
            Assert.False(game.GetScene(scene2.GetHashCode()).IsSuccess);

            game.LoadScene(scene);
            game.LoadNextScene(scene2);
            // 前にロードしたシーンの取得に失敗
            Assert.False(game.GetScene(scene.GetHashCode()).IsSuccess);
            // 新しくロードしたシーンの取得に成功
            Assert.True(game.GetScene(scene2.GetHashCode()).IsSuccess);

            // イベントの呼び出しチェック
            Assert.True(count == 1);
        }

        [Test]
        public void SceneUnsafeTest()
        {
            IGame game = new GameBuilder().Build();
            Scene scene = new Scene();

            // ロードに成功
            Assert.True(game.LoadSceneUnsafe(scene).IsSuccess);
            // 既にロードしたシーンなので失敗
            Assert.False(game.LoadSceneUnsafe(scene).IsSuccess);
            // 取得に成功
            Assert.True(game.GetSceneUnsafe<Scene>(scene.GetHashCode()).IsSuccess);
            // アンロードに成功
            Assert.True(game.UnloadSceneUnsafe(scene).IsSuccess);
            // 既にアンロードされているので失敗
            Assert.False(game.UnloadSceneUnsafe(scene).IsSuccess);
        }
    }
}