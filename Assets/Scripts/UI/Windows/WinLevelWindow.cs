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
        private GameManager _gameManager;
        private SaveLoadSystem _saveLoadSystem;
        private StateMachine _stateMachine;
        
        [Inject]
        private void Construct(CoinManager coinManager, GameManager gameManager, SaveLoadSystem saveLoadSystem, StateMachine stateMachine)
        {
            this._coinManager = coinManager;
            this._gameManager = gameManager;
            this._saveLoadSystem = saveLoadSystem;
            this._stateMachine = stateMachine;
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