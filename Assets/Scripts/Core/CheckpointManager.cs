using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class CheckpointManager : MonoBehaviour
    {
        [SerializeField] private List<Checkpoint> checkpoints;
        private readonly Dictionary<int, Checkpoint> _numberedCheckpoints = new();
        
        private void Awake()
        {
            foreach (var checkpoint in checkpoints)
            {
                _numberedCheckpoints[checkpoint.Number] = checkpoint;
            }
        }
        
        public Checkpoint GetCheckpointByNumber(int number)
        {
            return _numberedCheckpoints.GetValueOrDefault(number);
        }
    }
}
