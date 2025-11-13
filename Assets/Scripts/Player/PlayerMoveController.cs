using Reflex.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SmallBallBigPlane
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerInputs playerInputs;
        [SerializeField] private PlayerSoundEffectsHandler playerSoundEffectsHandler;
        
        private IGameManager _gameManager;
        private GameSettingsSO _gameSettings;
        
        private float MoveSpeed => _gameSettings.playerMoveSpeed;
        private float MaxVolume => _gameSettings.playerMoveSoundMaxVolume;
        private float JumpPower => _gameSettings.playerJumpPower;
        private AnimationCurve VolumeBySpeedCurve => _gameSettings.volumeBySpeedCurve;

        [Inject]
        private void Construct(IGameManager gameManager, GameSettingsSO gameSettings )
        {
            this._gameManager = gameManager;
            this._gameSettings = gameSettings;
        }
        
        private void Awake()
        {
            if (rb == null)
            {
                Debug.LogError("Rigidbody is null. Add and setup");
            }
            
            playerInput.enabled = true;
        }

        private void Start()
        {
            _gameManager.GameRestarted += GameManager_OnGameRestarted;
            _gameManager.GameWon += GameManager_OnGameWon;
        }

        private void OnDestroy()
        {
            _gameManager.GameRestarted -= GameManager_OnGameRestarted;
            _gameManager.GameWon -= GameManager_OnGameWon;       
        }

        private void GameManager_OnGameWon()
        {
            playerInput.enabled = false;
            
            ResetRigidBodyVelocities();
        }

        private void GameManager_OnGameRestarted()
        {
            ResetRigidBodyVelocities();

            if (playerInput.enabled) return;
            
            playerInput.enabled = true;
        }

        private void ResetRigidBodyVelocities()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            UpdateMovement();
            UpdateJump();
        }

        private void UpdateMovement()
        {
            Vector2 input = playerInputs.move;
            Vector3 movement = new Vector3(input.x, 0, input.y);

            rb.AddForce(movement * MoveSpeed, ForceMode.Force);
            
            float curveValue = VolumeBySpeedCurve.Evaluate(rb.velocity.magnitude);
            
            float soundVolume = Mathf.Clamp(curveValue * MaxVolume, 0, MaxVolume);
            
            playerSoundEffectsHandler.PlayMovingSound(soundVolume);
        }

        private void UpdateJump()
        {
            if (playerInputs.jump)
            {
                rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
                
                playerInputs.jump = false;
            }
        }
    }
}