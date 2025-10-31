using UnityEngine.SceneManagement;

namespace SmallBallBigPlane
{
    public interface ISceneLoader
    {
        void LoadScene(Scene scene);
        void LoaderCallback();
    }

    public enum Scene
    {
        MainMenuScene,
        LoadingScene,
        GameScene,
    }

    public class SceneLoader : ISceneLoader
    {
        private Scene targetScene;

        public void LoadScene(Scene scene)
        {
            targetScene = scene;

            SceneManager.LoadScene(nameof(Scene.LoadingScene));
        }

        public void LoaderCallback()
        {
            SceneManager.LoadScene(targetScene.ToString());
        }
    }
}