using UnityEngine;

namespace SmallBallBigPlane
{
    public class PlayerSoundEffectsHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClipSO movingSound;

        private void Start()
        {
            SetMovingSoundSettings();

            audioSource.Play();
        }

        private void SetMovingSoundSettings()
        {
            audioSource.clip = movingSound.audioClip;
            audioSource.volume = 0f;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }

        public void PlayMovingSound(float volumeLevel)
        {
            audioSource.volume = volumeLevel;
        }

        public void StopMovingSound()
        {
            if (audioSource.isPlaying == false) return;

            audioSource.Stop();
        }

        public void PlayMovingSound()
        {
            if (audioSource.isPlaying == true) return;
            
            audioSource.Play();
        }
    }
}