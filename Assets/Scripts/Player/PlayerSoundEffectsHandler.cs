using UnityEngine;

namespace SmallBallBigPlane
{
    public class PlayerSoundEffectsHandler : MonoBehaviour
    {
        [SerializeField] private AudioClipSO movingSound;
        [SerializeField] private AudioSource audioSource;

        private void Start()
        {
            audioSource.clip = movingSound.audioClip;
            audioSource.volume = 0f;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            
            audioSource.Play();
        }

        public void PlayMovingSound(float volumeLevel)
        {
            audioSource.volume = volumeLevel;
        }
    }
}