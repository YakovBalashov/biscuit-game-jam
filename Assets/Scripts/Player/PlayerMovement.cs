using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float defaultMaxSpeed;
        [SerializeField] private float accelerationRate;
        [SerializeField] private CinemachineOrbitalFollow orbitalCamera;
        [SerializeField] private Animator animator;
        
        private static readonly int SpeedID = Animator.StringToHash("Speed");
        private Rigidbody _rigidbody;
        private Vector2 _movementDirection;
        private PlayerDash _playerDash;

        public float CurrentMaxSpeed { get; set; }

        private void Awake()
        {
            CurrentMaxSpeed = defaultMaxSpeed;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _playerDash = GetComponent<PlayerDash>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movementDirection = context.ReadValue<Vector2>().normalized;
        }

        private void Update()
        {
            var currentSpeed = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.z).magnitude;
            animator.SetFloat(SpeedID, currentSpeed);
        }

        private void FixedUpdate()
        {
            if (_playerDash.IsDashing) return;
            var desiredVelocity = GetRotatedMovementDirection() * CurrentMaxSpeed;
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

        public void ResetMaxSpeed()
        {
            CurrentMaxSpeed = defaultMaxSpeed;
        }
    }
}