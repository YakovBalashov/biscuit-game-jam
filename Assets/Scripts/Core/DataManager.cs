using UnityEngine;

namespace Core
{
    public class DataManager : MonoBehaviour
    {

        public static DataManager Instance;
        
        public int LastCheckpointNumber { get; private set; }
        
        public void UpdateLastCheckpoint(int checkpointNumber)
        {
            if (checkpointNumber <= LastCheckpointNumber) return;
            LastCheckpointNumber = checkpointNumber;
        }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}