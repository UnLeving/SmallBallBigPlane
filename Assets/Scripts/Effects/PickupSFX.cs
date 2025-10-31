using UnityEngine;

namespace SmallBallBigPlane
{
    public class PickupSFX : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float lifeTime;
        [SerializeField, Min(0.1f)] private float pitchVariation;
        [SerializeField] private AudioSource audioSource;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
            
            audioSource.pitch += Random.Range(-pitchVariation, +pitchVariation);
            
            audioSource.Play();
        }
    }
}