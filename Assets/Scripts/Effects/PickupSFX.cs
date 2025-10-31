using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace SmallBallBigPlane
{
    [RequireComponent(typeof(AudioSource))]
    public class PickupSFX : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float lifeTime;
        [SerializeField, Min(0.1f)] private float pitchVariation;
        [SerializeField] private AudioClipSO audioClip;

        private AudioSource audioSource;
        private IObjectPool<PickupSFX> _pool;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Initialize(IObjectPool<PickupSFX> pool)
        {
            _pool = pool;

            audioSource.clip = audioClip.audioClip;
            audioSource.pitch = 1f + Random.Range(-pitchVariation, +pitchVariation);
            audioSource.Play();

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