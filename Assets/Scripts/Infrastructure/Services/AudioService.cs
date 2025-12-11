using Reflex.Attributes;
using SmallBallBigPlane.Infrastructure.Services;
using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.Services
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioService : MonoBehaviour
    {
        private AudioSource audioSource;
        private GameStateService _gameManager;
        private GameSettingsSO _gameSettings;
        
        private AudioClipSO WinSound => _gameSettings.winSound;
        private AudioClipSO LooseSound => _gameSettings.loseSound;
                

        [Inject]
        private void Construct(GameStateService gameManager, GameSettingsSO gameSettings)
        {
            this._gameManager = gameManager;
            this._gameSettings = gameSettings;
        }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _gameManager.GameLost += PlayLooseSound;
            _gameManager.GameWon += PlayWinSound;
        }
        
        private void OnDestroy()
        {
            _gameManager.GameLost -= PlayLooseSound;
            _gameManager.GameWon -= PlayWinSound;       
        }
        
        public void PlayWinSound()
        {
            if(audioSource.isPlaying) return;
            
            audioSource.clip = WinSound.audioClip;
            audioSource.volume = 1;
            audioSource.Play();
        }
        
        public void PlayLooseSound()
        {
            if(audioSource.isPlaying) return;
            
            audioSource.clip = LooseSound.audioClip;
            audioSource.volume = 1;
            audioSource.Play();
        }
    }
}