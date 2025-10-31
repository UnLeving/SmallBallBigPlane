using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace SmallBallBigPlane
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        [Inject] private ISceneLoader _sceneLoader;
        
        private void OnEnable()
        {
            startButton.onClick.AddListener(OnStartClicked);
            exitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(OnStartClicked);
            exitButton.onClick.RemoveListener(OnExitClicked);
        }

        // todo add loader scene
        private void OnStartClicked()
        {
            _sceneLoader.LoadScene(SceneName.GameScene);
        }

        private void OnExitClicked()
        {
            Application.Quit();
        }
    }
}