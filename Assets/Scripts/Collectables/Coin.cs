using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane.Collectables
{
    public class Coin : MonoBehaviour, ICollectable
    {
       [SerializeField] private float rotationSpeed = 10f;
       [Inject] private CoinManager _coinManager;
       [Inject] private PickupSoundEffect _pickupSoundEffect;
       
        public void Collect()
        {
            _coinManager.CollectCoin();
            
            gameObject.SetActive(false);

            Instantiate(_pickupSoundEffect);
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}