using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerSoundEffectsHandler : MonoBehaviour
    {
        private AudioSource audioSource;
        private GameSettingsSO _gameSettings;

        private AudioClipSO MovingSound => _gameSettings.playerMovingSound;

        [Inject]
        public void Construct(GameSettingsSO gameSettings)
        {
            this._gameSettings = gameSettings;
        }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            SetMovingSoundSettings();

            audioSource.Play();
        }

        private void SetMovingSoundSettings()
        {
            audioSource.clip = MovingSound.audioClip;
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