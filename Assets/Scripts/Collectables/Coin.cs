using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane.Collectables
{
    public class Coin : MonoBehaviour, ICollectable
    {
       private ICoinManager _coinManager;
       private IPickupEffectsHandler _pickupEffectsHandler;
       private GameSettingsSO _gameSettings;
       
       private float RotationSpeed => _gameSettings.coinRotationSpeed;
       
       [Inject]
       private void Construct(ICoinManager coinManager, IPickupEffectsHandler pickupEffectsHandler, GameSettingsSO gameSettings)
       {
            this._coinManager = coinManager;
            this._pickupEffectsHandler = pickupEffectsHandler;
            this._gameSettings = gameSettings;
       }
       
        public void Collect()
        {
            _coinManager.CollectCoin();
            
            gameObject.SetActive(false);
            
            _pickupEffectsHandler.PlayPickupEffects(transform.position);
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