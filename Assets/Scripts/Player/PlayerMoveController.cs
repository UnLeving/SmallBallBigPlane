using UnityEngine;

namespace SmallBallBigPlane
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PlayerInputs playerInputs;

        private void Awake()
        {
            if (rb == null)
            {
                Debug.LogError("Rigidbody is null. Add and setup");
            }
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
        }
    }
}