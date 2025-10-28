using SmallBallBigPlane.Collectables;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class Player : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }
    }
}