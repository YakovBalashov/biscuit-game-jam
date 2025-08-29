using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        [SerializeField] private GameObject player;
        [SerializeField] private CheckpointManager checkpointManager;

        private Checkpoint LastCheckpoint { get; set; }


        public void UpdateLastCheckpoint(Checkpoint checkpoint)
        {
            if (LastCheckpoint != null && checkpoint < LastCheckpoint) return;
            LastCheckpoint = checkpoint;
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
            }
        }

        private void Start()
        {
            var lastCheckpointNumber = DataManager.Instance.LastCheckpointNumber;
            LastCheckpoint = checkpointManager.GetCheckpointByNumber(lastCheckpointNumber);
            player.transform.position = LastCheckpoint.spawnPoint.position;
        }   

        public static void QuitGame()
        {
            Application.Quit();
        }

        public void RestartGame()
        {
            player.transform.position = LastCheckpoint.spawnPoint.position;
        }
        
    }
}
