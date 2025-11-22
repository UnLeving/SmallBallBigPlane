using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using SmallBallBigPlane.Infrastructure.FSM;
using SmallBallBigPlane.Infrastructure.FSM.States;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.Infrastructure.Services.Factories;
using SmallBallBigPlane.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace SmallBallBigPlane.UI
{
    public class MainMenu : UIContainer
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;

        private StateMachine _stateMachine;
        [Inject] private readonly WindowsService _windowsService;


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
            _stateMachine.Enter<LoadLevelState>();
        }

        private void OnExitClicked()
        {
            _stateMachine.Enter<ExitState>();
        }

        private void OnSettingsClicked()
        {
            //todo fix settings wnd call
            _windowsService.Show(WindowId.Settings);
        }

        public override UniTask Show()
        {
            gameObject.SetActive(true);

            return UniTask.CompletedTask;
        }

        public override UniTask Hide()
        {
            gameObject.SetActive(false);

            return UniTask.CompletedTask;
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}