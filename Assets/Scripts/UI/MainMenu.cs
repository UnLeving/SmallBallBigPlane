using HelpersAndExtensions.SaveSystem;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace SmallBallBigPlane
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private SettingsWindow settingsWindow;

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
            settingsButton.onClick.AddListener(OnSettingsClicked);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(OnStartClicked);
            exitButton.onClick.RemoveListener(OnExitClicked);
            settingsButton.onClick.RemoveListener(OnSettingsClicked);
        }

        private void OnStartClicked()
        {
            _saveLoadSystem.TryLoadGame();

            _sceneLoader.LoadSceneAsync(SceneLoader.Scene.LoadingScene);
        }

        private void OnExitClicked()
        {
            Application.Quit();
        }

        private void OnSettingsClicked()
        {
            settingsWindow.gameObject.SetActive(true);
        }
    }
}