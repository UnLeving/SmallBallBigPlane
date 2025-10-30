using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class GameSoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClipSO winSound;
        [SerializeField] private AudioClipSO looseSound;
                
        [Inject] private GameManager _gameManager;
        
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
            audioSource.clip = winSound.audioClip;
            audioSource.volume = 1;
            audioSource.Play();
        }
        
        public void PlayLooseSound()
        {
            audioSource.clip = looseSound.audioClip;
            audioSource.volume = 1;
            audioSource.Play();
        }
    }
}