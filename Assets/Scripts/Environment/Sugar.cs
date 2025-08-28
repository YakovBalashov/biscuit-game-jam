using System.Collections;
using Player;
using UnityEngine;

namespace Environment
{
    public class Sugar : Collectable
    {
        [SerializeField] private float duration;
        [SerializeField] private float boostedSpeed;

        private PlayerMovement _playerMovement;
        private Collider _collider;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        protected override void Action(GameObject player)
        {
            _playerMovement = player.GetComponent<PlayerMovement>();
            if (_playerMovement == null) return;

            _playerMovement.CurrentMaxSpeed = boostedSpeed;
            SetInteractivity(false);
            StartCoroutine(SlowDownAfterTime(duration));
        }

        private IEnumerator SlowDownAfterTime(float delay)
        {
            yield return new WaitForSeconds(delay);
            _playerMovement.ResetMaxSpeed();
            SetInteractivity(true);
        }

        private void SetInteractivity(bool enable)
        {
            _meshRenderer.enabled = enable;
            _collider.enabled = enable;
        }
    }
}