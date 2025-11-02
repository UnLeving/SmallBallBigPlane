using HelpersAndExtensions.SaveSystem;
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
        
        private void OnStartClicked()
        {
            SaveLoadSystem.Instance.TryLoadGame();
            
            _sceneLoader.LoadScene(Scene.GameScene);
        }

        private void OnExitClicked()
        {
            Application.Quit();
        }
    }
}