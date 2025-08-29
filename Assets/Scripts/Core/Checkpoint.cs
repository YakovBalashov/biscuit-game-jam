using Player;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Collider))]
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private int number;
        public int Number => number;        
        public Transform spawnPoint;
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovement>() == null) return;
            GameManager.Instance.UpdateLastCheckpoint(this);
        }

        public static bool operator >(Checkpoint a, Checkpoint b) => a.Number > b.Number;

        public static bool operator <(Checkpoint a, Checkpoint b) => a.Number < b.Number;
    }
}