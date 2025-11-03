using UnityEngine;
using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
using TMPro;

namespace SmallBallBigPlane.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private ICoinManager _coinManager;
        
        [Inject]
        private void Construct(ICoinManager coinManager)
        {
            this._coinManager = coinManager;
        }
        
        private void Start()
        {
            _coinManager.OnCoinCollected += CoinManager_OnOnCoinCollected;
        }
        
        private void OnDestroy()
        {
            _coinManager.OnCoinCollected -= CoinManager_OnOnCoinCollected;
        }

        private void CoinManager_OnOnCoinCollected(int score)
        {
            UpdateText(score);
        }

        private void UpdateText(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}