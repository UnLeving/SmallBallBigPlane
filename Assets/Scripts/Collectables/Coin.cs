using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane.Collectables
{
    public class Coin : MonoBehaviour, ICollectable
    {
       [SerializeField] private float rotationSpeed = 10f;
       [Inject] private ICoinManager _coinManager;
       [Inject] private PickupSFX pickupSfx;
       [Inject] private PickupVFX _pickupVFX;
       
        public void Collect()
        {
            _coinManager.CollectCoin();
            
            gameObject.SetActive(false);

            Instantiate(pickupSfx);
            Instantiate(_pickupVFX, transform.position, Quaternion.identity);
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}