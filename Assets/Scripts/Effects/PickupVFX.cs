using UnityEngine;
using UnityEngine.Pool;

namespace SmallBallBigPlane
{
    public class PickupVFX : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float lifeTime;

        private IObjectPool<PickupVFX> _pool;

        public void Initialize(IObjectPool<PickupVFX> pool)
        {
            _pool = pool;
            Invoke(nameof(ReturnToPool), lifeTime);
        }

        private void ReturnToPool()
        {
            _pool.Release(this);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }
    }
}