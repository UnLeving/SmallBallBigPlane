using Cinemachine;
using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class VirtualCameraWarp : MonoBehaviour
    {
        private GameManager _gameManager;
        private CinemachineVirtualCamera _virtualCamera;
        
        [Inject]
        private void Construct(GameManager gameManager)
        {
            this._gameManager = gameManager;
        }

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            _gameManager.GameRestarted += GameManager_OnGameRestarted;
        }

        private void OnDestroy()
        {
            _gameManager.GameRestarted -= GameManager_OnGameRestarted;
        }

        private void GameManager_OnGameRestarted()
        {
            _virtualCamera.PreviousStateIsValid = false;
        }
    }
}