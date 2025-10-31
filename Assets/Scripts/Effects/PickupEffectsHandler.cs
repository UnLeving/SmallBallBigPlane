using UnityEngine;
using UnityEngine.Pool;

namespace SmallBallBigPlane.Collectables
{
    public class PickupEffectsHandler : MonoBehaviour, IPickupEffectsHandler
    {
        [SerializeField] private PickupVFX pickupVFXPrefab;
        [SerializeField] private PickupSFX pickupSfxPrefab;
        [SerializeField] private int poolSize = 5;
        private IObjectPool<PickupVFX> _pickupVFXPool;
        private IObjectPool<PickupSFX> _pickupSFXPool;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _pickupVFXPool = new ObjectPool<PickupVFX>(
                createFunc: () => Instantiate(pickupVFXPrefab),
                actionOnGet: vfx => vfx.gameObject.SetActive(true),
                actionOnRelease: vfx => vfx.gameObject.SetActive(false),
                actionOnDestroy: vfx => Destroy(vfx.gameObject),
                collectionCheck: false,
                defaultCapacity: poolSize,
                maxSize: poolSize * 2
            );

            _pickupSFXPool = new ObjectPool<PickupSFX>(
                createFunc: () => Instantiate(pickupSfxPrefab),
                actionOnGet: sfx => sfx.gameObject.SetActive(true),
                actionOnRelease: sfx => sfx.gameObject.SetActive(false),
                actionOnDestroy: sfx => Destroy(sfx.gameObject),
                collectionCheck: false,
                defaultCapacity: poolSize,
                maxSize: poolSize * 2
            );
        }

        public void PlayPickupEffects(Vector3 position)
        {
            PlayVFX(position);
            PlaySFX(position);
        }

        private void PlayVFX(Vector3 position)
        {
            var vfx = _pickupVFXPool.Get();
            vfx.transform.position = position;
            vfx.Initialize(_pickupVFXPool);
        }

        private void PlaySFX(Vector3 position)
        {
            var sfx = _pickupSFXPool.Get();
            sfx.transform.position = position;
            sfx.Initialize(_pickupSFXPool);
        }
    }
}