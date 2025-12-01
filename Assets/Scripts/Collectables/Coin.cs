using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane.Collectables
{
    public class Coin : MonoBehaviour, ICollectable
    {
       private CoinManager _coinManager;
       private GameSettingsSO _gameSettings;
       
       private float RotationSpeed => _gameSettings.coinRotationSpeed;
       
       [Inject]
       private void Construct(CoinManager coinManager, GameSettingsSO gameSettings)
       {
            this._coinManager = coinManager;
            this._gameSettings = gameSettings;
       }
       
        public void Collect()
        {
            _coinManager.CollectCoin(this);
            
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            if(gameObject.activeSelf == true) return;
            
            gameObject.SetActive(true);
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
        }
    }
}