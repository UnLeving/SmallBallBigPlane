using UnityEngine.SceneManagement;

namespace SmallBallBigPlane
{
    public interface ISceneLoader
    {
        void LoadScene(SceneName sceneName);
    }

    public enum SceneName
    {
        MainMenuScene,
        GameScene,
    }
    
    public class SceneLoader: ISceneLoader
    {
        public void LoadScene(SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}
