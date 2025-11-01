using System;
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
        [Inject] private ICoinManager _coinManager;
        [Inject] private IGameManager _gameManager;

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
            
            //Hide();
        }

        public override void Show()
        {
            base.Show();

            UpdateScoreText(_coinManager.CoinCount);
        }

        private void UpdateScoreText(int score, int bestScore = 0)
        {
            scoreText.text = score.ToString();
            bestScoreText.text = bestScore.ToString();
        }
    }
}