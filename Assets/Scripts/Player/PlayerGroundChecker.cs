using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class PlayerGroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float acceptableTimeInAir = 1f;
        [SerializeField] private Transform groundCheckPoint;

        private float _timeInAir;
        private int _isGrounded;
        private RaycastHit[] _raycastHits = new RaycastHit[1];
        
        [Inject] private GameManager _gameManager;

        private void Start()
        {
            if (groundCheckPoint == null)
            {
                groundCheckPoint = transform;
            }
        }

        private void FixedUpdate()
        {
            CheckGroundStatus();
        }

        private void CheckGroundStatus()
        {
            _isGrounded = Physics.RaycastNonAlloc(groundCheckPoint.position, 
                Vector3.down, 
                _raycastHits, 
                10f, 
                groundLayer, 
                QueryTriggerInteraction.Ignore);

            if (_isGrounded == 1)
            {
                _timeInAir = 0f;
            }
            else
            {
                _timeInAir += Time.deltaTime;

                if (_timeInAir > acceptableTimeInAir)
                {
                    _gameManager.PlayerFall();
                }
            }
            //
            // Debug.DrawRay(groundCheckPoint.position, 
            //     Vector3.down, Color.red);
        }
    }
}