using Cysharp.Threading.Tasks;
using HelpersAndExtensions.SaveSystem;
using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmallBallBigPlane
{
    public class WinLevelWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;
        [SerializeField] private Button restartButton;
        
        private ICoinManager _coinManager;
        private IGameManager _gameManager;
        
        [Inject]
        private void Construct(ICoinManager coinManager, IGameManager gameManager)
        {
            this._coinManager = coinManager;
            this._gameManager = gameManager;
        }

        private void OnEnable()
        {
            restartButton.onClick.AddListener(OnRestartClicked);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(OnRestartClicked);
        }

        private void OnRestartClicked()
        {
            _gameManager.RestartRequested();

            SaveLoadSystem.Instance.SaveGame();

            //Hide();
        }

        public override async UniTask Show()
        {
            await base.Show();

            _coinManager.SetMaxCoinCount();
            
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            scoreText.text = _coinManager.CoinCount.ToString();
            bestScoreText.text = _coinManager.MaxCoinCount.ToString();
        }
    }
}