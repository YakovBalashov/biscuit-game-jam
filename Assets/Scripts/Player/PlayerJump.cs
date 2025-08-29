using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float fallSpeed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private float groundCheckDistance = 0.3f;
        [SerializeField] private LayerMask groundLayerMask;

        private Rigidbody _rigidbody;
        private bool _isJumpPressed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_rigidbody.linearVelocity.y < 0 || !_isJumpPressed)
            {
                _rigidbody.linearVelocity += Vector3.up * (Physics.gravity.y * fallSpeed * Time.fixedDeltaTime);
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            _isJumpPressed = context.ReadValueAsButton();
            if (!_isJumpPressed) return;
            Jump();
        }

        private void Jump()
        {
            if (!IsGrounded()) return;
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private bool IsGrounded()
        {
            if (Math.Abs(_rigidbody.linearVelocity.y) > 0.001f) return false;
            Vector3 checkPos = transform.position + Vector3.down * groundCheckDistance;
            return Physics.CheckSphere(checkPos, groundCheckRadius, groundLayerMask);
        }
    }
}