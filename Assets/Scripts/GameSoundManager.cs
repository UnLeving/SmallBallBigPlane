using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    [RequireComponent(typeof(AudioSource))]
    public class GameSoundManager : MonoBehaviour
    {
        private AudioSource audioSource;
        private IGameManager _gameManager;
        private GameSettingsSO _gameSettings;
        
        private AudioClipSO WinSound => _gameSettings.winSound;
        private AudioClipSO LooseSound => _gameSettings.loseSound;
                

        [Inject]
        private void Construct(IGameManager gameManager, GameSettingsSO gameSettings)
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