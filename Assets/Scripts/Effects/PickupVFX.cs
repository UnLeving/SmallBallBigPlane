using UnityEngine;

namespace SmallBallBigPlane
{
    public class PickupVFX : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float lifeTime;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}