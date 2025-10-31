using System;
using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    [RequireComponent(typeof(AudioSource))]
    public class GameSoundManager : MonoBehaviour
    {
        private AudioSource audioSource;
        [SerializeField] private AudioClipSO winSound;
        [SerializeField] private AudioClipSO looseSound;
                
        [Inject] private IGameManager _gameManager;

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
            
            audioSource.clip = winSound.audioClip;
            audioSource.volume = 1;
            audioSource.Play();
        }
        
        public void PlayLooseSound()
        {
            if(audioSource.isPlaying) return;
            
            audioSource.clip = looseSound.audioClip;
            audioSource.volume = 1;
            audioSource.Play();
        }
    }
}