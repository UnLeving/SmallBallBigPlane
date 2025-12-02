using Cysharp.Threading.Tasks;
using HelpersAndExtensions.SaveSystem;
using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
using SmallBallBigPlane.Infrastructure.FSM;
using SmallBallBigPlane.Infrastructure.FSM.States;
using SmallBallBigPlane.Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmallBallBigPlane.UI.Windows
{
    public class WinLevelWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private AdWheel.AdWheel adWheel;

        private CoinManager _coinManager;
        private SaveLoadSystem _saveLoadSystem;
        private StateMachine _stateMachine;
        private LoadingScreen _loadingScreen;
        private LevelsManager _levelsManager;

        [Inject]
        private void Construct(CoinManager coinManager, SaveLoadSystem saveLoadSystem, StateMachine stateMachine,
            LoadingScreen loadingScreen, LevelsManager levelsManager)
        {
            this._coinManager = coinManager;
            this._saveLoadSystem = saveLoadSystem;
            this._stateMachine = stateMachine;
            this._loadingScreen = loadingScreen;
            this._levelsManager = levelsManager;
        }

        private void OnEnable()
        {
            restartButton.onClick.AddListener(OnRestartClicked);
            nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(OnRestartClicked);
            nextLevelButton.onClick.RemoveListener(OnNextLevelClicked);
        }

        private async void OnNextLevelClicked()
        {
            _levelsManager.LevelPassed();

            _saveLoadSystem.SaveGame();

            await Hide();

            _stateMachine.Enter<LoadLevelState>();
        }

        private async void OnRestartClicked()
        {
            _saveLoadSystem.SaveGame();

            await Hide();

            await _loadingScreen.Show();

            _stateMachine.Enter<RestartState>();
        }

        public override async UniTask Show()
        {
            if (isOpened) return;

            isOpened = true;

            UpdateScoreText();

            await base.ShowPanel();
        }

        public override async UniTask Hide()
        {
            if (!isOpened) return;

            isOpened = false;

            await base.HidePanel();
        }

        private void UpdateScoreText()
        {
            adWheel.Initialize(_coinManager.CoinCount);
            
            scoreText.text = _coinManager.CoinCount.ToString();
            bestScoreText.text = _coinManager.MaxCoinCount.ToString();
        }
    }
}