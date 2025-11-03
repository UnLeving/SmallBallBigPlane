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
        [SerializeField] private float moveSpeed = 2f;
        
        [Inject] private IGameManager _gameManager;

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
        }

        private void UpdateMovement()
        {
            Vector2 input = playerInputs.move;
            Vector3 movement = new Vector3(input.x, 0, input.y);

            rb.AddForce(movement * moveSpeed, ForceMode.Force);
            
            var soundVolume = Mathf.Clamp(rb.velocity.magnitude, 0, .5f);
            
            playerSoundEffectsHandler.PlayMovingSound(soundVolume);
        }
    }
}