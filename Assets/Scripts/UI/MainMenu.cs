using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SmallBallBigPlane
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

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
            SceneManager.LoadScene("GameScene");
        }

        private void OnExitClicked()
        {
            Application.Quit();
        }
    }
}