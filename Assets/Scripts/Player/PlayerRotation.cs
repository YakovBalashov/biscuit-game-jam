using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void FixedUpdate()
        {
            Vector2 movementDirection = _playerMovement.GetRotatedMovementDirection();
            if (movementDirection == Vector2.zero) return;

            float targetRotationY = Mathf.Atan2(movementDirection.x, movementDirection.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetRotationY, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}