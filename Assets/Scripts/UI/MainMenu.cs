using Reflex.Attributes;
using SmallBallBigPlane.Infrastructure.FSM;
using SmallBallBigPlane.Infrastructure.FSM.States;
using SmallBallBigPlane.UI.Windows;
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

        private StateMachine _stateMachine;


        [Inject]
        private void Construct(StateMachine stateMachine)
        {
            this._stateMachine = stateMachine;
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
            _stateMachine.Enter<LoadingState>();
        }

        private void OnExitClicked()
        {
            _stateMachine.Enter<ExitState>();
        }

        private void OnSettingsClicked()
        {
            settingsWindow.gameObject.SetActive(true);
        }
    }
}