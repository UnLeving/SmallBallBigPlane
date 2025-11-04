using UnityEngine.SceneManagement;

namespace SmallBallBigPlane
{
    public interface ISceneLoader
    {
        void LoadScene(SceneLoader.Scene scene);
        void LoadSceneAsync(SceneLoader.Scene scene);
        void LoaderCallback();
    }

    public class SceneLoader : ISceneLoader
    {
        public enum Scene
        {
            MainMenuScene,
            LoadingScene,
            GameScene,
        }
        
        private Scene targetScene;

        public void LoadScene(Scene scene)
        {
            targetScene = scene;

            SceneManager.LoadScene(nameof(Scene.LoadingScene));
        }

        public void LoadSceneAsync(Scene scene)
        {
            targetScene = scene;

            SceneManager.LoadSceneAsync(nameof(Scene.LoadingScene));
        }

        public void LoaderCallback()
        {
            SceneManager.LoadSceneAsync(targetScene.ToString());
        }
    }
}