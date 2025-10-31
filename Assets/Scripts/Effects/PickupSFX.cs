using UnityEngine;
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

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            Destroy(gameObject, lifeTime);

            audioSource.clip = audioClip.audioClip;

            audioSource.pitch += Random.Range(-pitchVariation, +pitchVariation);

            audioSource.Play();
        }
    }
}