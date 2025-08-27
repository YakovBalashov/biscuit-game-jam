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
        [SerializeField] private float groundedRaycastDistance;

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
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private bool IsGrounded()
        {
            if (Math.Abs(_rigidbody.linearVelocity.y) > 0.001f) return false;
            return Physics.Raycast(transform.position, Vector3.down, out _, groundedRaycastDistance);
        }
    }
}