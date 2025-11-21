using Cysharp.Threading.Tasks;
using HelpersAndExtensions.SaveSystem;
using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
using SmallBallBigPlane.Infrastructure.FSM;
using SmallBallBigPlane.Infrastructure.FSM.States;
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
        
        private CoinManager _coinManager;
        private SaveLoadSystem _saveLoadSystem;
        private StateMachine _stateMachine;
        private LoadingScreen _loadingScreen;
        
        [Inject]
        private void Construct(CoinManager coinManager, SaveLoadSystem saveLoadSystem, StateMachine stateMachine,  LoadingScreen loadingScreen)
        {
            this._coinManager = coinManager;
            this._saveLoadSystem = saveLoadSystem;
            this._stateMachine = stateMachine;
            this._loadingScreen = loadingScreen;
        }

        private void OnEnable()
        {
            restartButton.onClick.AddListener(OnRestartClicked);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(OnRestartClicked);
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
            
            _coinManager.SetMaxCoinCount();
            
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
            scoreText.text = _coinManager.CoinCount.ToString();
            bestScoreText.text = _coinManager.MaxCoinCount.ToString();
        }
    }
}