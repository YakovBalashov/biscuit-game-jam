using Core;
using UnityEngine;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {
        public void Die()
        {
            GameManager.Instance.RestartGame();
        }
    }
}
