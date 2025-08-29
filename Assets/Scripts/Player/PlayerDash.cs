using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerDash : MonoBehaviour
    {
        [SerializeField] private float dashForce;
        [SerializeField] private float dashCooldown;
        [SerializeField] private float dashDuration;

        private Rigidbody _rigidbody;
        private bool _isDashPressed;
        public bool IsDashing { get; set; }

        private void Awake()
        {
            IsDashing = false;
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            _isDashPressed = context.ReadValueAsButton();
            if (!_isDashPressed) return;
            if (IsDashing) return;
            StartCoroutine(Dash());
        }

        private IEnumerator Dash()
        {
            IsDashing = true;
            _rigidbody.linearVelocity = new Vector3(transform.forward.x * dashForce, 0, transform.forward.z * dashForce);
            var defaultRigidbodyConstraints = _rigidbody.constraints;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            yield return new WaitForSeconds(dashDuration);
            _rigidbody.constraints = defaultRigidbodyConstraints;
            _rigidbody.linearVelocity = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(dashCooldown);
            IsDashing = false;
        }
    }
}