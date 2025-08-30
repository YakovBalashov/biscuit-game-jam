using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float fallSpeed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private float coyoteTime;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private float fallingThreshold;
        
        
        private float _currentCoyoteTime;
        private Rigidbody _rigidbody;
        private bool _isJumpPressed;
        private bool _isJumping;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (IsGrounded())
            {
                _currentCoyoteTime = coyoteTime;
                _isJumping = false;
            }
            else
            {
                _currentCoyoteTime -= Time.fixedDeltaTime;
            }
            
            if (_rigidbody.linearVelocity.y < fallingThreshold || !_isJumpPressed)
            {
                _rigidbody.linearVelocity += Vector3.up * (Physics.gravity.y * fallSpeed * Time.fixedDeltaTime);
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            _isJumpPressed = context.ReadValueAsButton();
            if (!_isJumpPressed || _isJumping) return;
            Jump();
        }

        private void Jump()
        {
            if (!IsGrounded() && _currentCoyoteTime <= 0f) return;

            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _currentCoyoteTime = 0f;
            _isJumping = true;
        }

        private bool IsGrounded()
        {
            Vector3 checkPos = transform.position + Vector3.down * groundCheckDistance;
            return Physics.CheckSphere(checkPos, groundCheckRadius, groundLayerMask);
        }
    }
}