using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(Collider))]
    public class KillingObject : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            var playerDeath = other.gameObject.GetComponent<Player.PlayerDeath>();
            if (playerDeath == null) return;
            playerDeath.Die();
        }
    }
}
