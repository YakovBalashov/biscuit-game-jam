using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float accelerationRate;

        private Vector3 _movementDirection;
        private Vector3 _desiredVelocity;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            var movementDirection2D = context.ReadValue<Vector2>().normalized;
            _desiredVelocity = new Vector3(movementDirection2D.x, 0, movementDirection2D.y) * speed;
        }

        private void FixedUpdate()
        {
            var currentVelocity = _rigidbody.linearVelocity;
            var deltaVelocity = _desiredVelocity - currentVelocity;
            _rigidbody.linearVelocity = currentVelocity + deltaVelocity * accelerationRate;
        }
    }
}