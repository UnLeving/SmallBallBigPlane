using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class PlayerGroundChecker : MonoBehaviour
    {
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private PlayerSoundEffectsHandler playerSoundEffectsHandler;

        private float _timeInAir;
        private int _isGrounded;
        private RaycastHit[] _raycastHits = new RaycastHit[1];
        private bool _blockGroundCheck;

        private IGameManager _gameManager;
        private GameSettingsSO _gameSettings;
        
        private LayerMask GroundLayer => _gameSettings.playerGroundLayer;
        private float AcceptableTimeInAir => _gameSettings.timeInAirBeforePlayerDie;
        
        [Inject]
        private void Construct(IGameManager gameManager, GameSettingsSO gameSettings)
        {
            this._gameManager = gameManager;
            this._gameSettings = gameSettings;
        }

        private void Start()
        {
            if (groundCheckPoint == null)
            {
                groundCheckPoint = transform;
            }
            
            _gameManager.GameRestarted += GameManager_OnGameRestarted;
        }

        private void GameManager_OnGameRestarted()
        {
            _blockGroundCheck = false;
        }

        private void FixedUpdate()
        {
            if (_blockGroundCheck) return;
            
            CheckGroundStatus();
        }

        private void CheckGroundStatus()
        {
            _isGrounded = Physics.RaycastNonAlloc(groundCheckPoint.position,
                Vector3.down,
                _raycastHits,
                10f,
                GroundLayer,
                QueryTriggerInteraction.Ignore);

            if (_isGrounded == 1)
            {
                _timeInAir = 0f;
                
                _blockGroundCheck = false;
                
                playerSoundEffectsHandler.PlayMovingSound();
            }
            else
            {
                playerSoundEffectsHandler.StopMovingSound();
                
                _timeInAir += Time.deltaTime;

                if (_timeInAir > AcceptableTimeInAir)
                {
                    _gameManager.PlayerFall();
                    
                    _blockGroundCheck = true;
                }
            }
            //
            // Debug.DrawRay(groundCheckPoint.position, 
            //     Vector3.down, Color.red);
        }
    }
}