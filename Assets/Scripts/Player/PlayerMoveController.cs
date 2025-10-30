using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PlayerInputs playerInputs;
        [SerializeField] private PlayerSoundEffectsHandler playerSoundEffectsHandler;
        
        [Inject] private GameManager _gameManager;

        private void Awake()
        {
            if (rb == null)
            {
                Debug.LogError("Rigidbody is null. Add and setup");
            }
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

            rb.AddForce(movement, ForceMode.Force);
            
            var soundVolume = Mathf.Clamp(rb.velocity.magnitude, 0, 1);
            
            playerSoundEffectsHandler.PlayMovingSound(soundVolume);
        }
    }
}