using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float accelerationRate;
        [SerializeField] private CinemachineOrbitalFollow orbitalCamera;

        private Rigidbody _rigidbody;
        private Vector2 _movementDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movementDirection = context.ReadValue<Vector2>().normalized;
        }

        private void FixedUpdate()
        {
            var desiredVelocity = GetRotatedMovementDirection() * speed;
            var currentVelocity = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.z);
            var deltaVelocity = (desiredVelocity - currentVelocity) * accelerationRate;
            var newHorizontalVelocity = currentVelocity + deltaVelocity;
            _rigidbody.linearVelocity = new Vector3(newHorizontalVelocity.x, _rigidbody.linearVelocity.y,
                newHorizontalVelocity.y);
        }

        public Vector2 GetRotatedMovementDirection()
        {
            InputAxis cameraDirection = orbitalCamera.HorizontalAxis;

            float cameraAngle = -cameraDirection.Value;
            float cameraAngleRad = cameraAngle * Mathf.Deg2Rad;

            float sinAngle = Mathf.Sin(cameraAngleRad);
            float cosAngle = Mathf.Cos(cameraAngleRad);

            var rotatedMovementDirection = new Vector2(
                _movementDirection.x * cosAngle - _movementDirection.y * sinAngle,
                _movementDirection.x * sinAngle + _movementDirection.y * cosAngle
            );

            return rotatedMovementDirection;
        }
    }
}