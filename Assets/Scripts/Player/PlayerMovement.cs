using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float accelerationRate;

        private Vector2 _desiredVelocity;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            var normalizedMovementDirection = context.ReadValue<Vector2>().normalized;
            _desiredVelocity = normalizedMovementDirection * speed;
        }

        private void FixedUpdate()
        {
            var currentVelocity = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.z);
            var deltaVelocity = (_desiredVelocity - currentVelocity) * accelerationRate;
            var newHorizontalVelocity = currentVelocity + deltaVelocity;
            _rigidbody.linearVelocity = new Vector3(newHorizontalVelocity.x, _rigidbody.linearVelocity.y, newHorizontalVelocity.y);
        }
    }
}