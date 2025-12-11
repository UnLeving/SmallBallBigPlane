using Reflex.Attributes;
using SmallBallBigPlane.Infrastructure.Services;
using Unity.Cinemachine;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class VirtualCameraWarp : MonoBehaviour
    {
        private GameStateService _gameManager;
        private CinemachineCamera _virtualCamera;
        
        [Inject]
        private void Construct(GameStateService gameManager)
        {
            this._gameManager = gameManager;
        }

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineCamera>();
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