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

        private ISceneLoader _sceneLoader;
        private ISaveLoadSystem _saveLoadSystem;
        
        
        [Inject]
        private void Construct(ISceneLoader sceneLoader, ISaveLoadSystem saveLoadSystem)
        {
            this._sceneLoader = sceneLoader;
            this._saveLoadSystem = saveLoadSystem;
        }
        
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
            _saveLoadSystem.TryLoadGame();
            
            _sceneLoader.LoadScene(Scene.GameScene);
        }

        private void OnExitClicked()
        {
            Application.Quit();
        }
    }
}