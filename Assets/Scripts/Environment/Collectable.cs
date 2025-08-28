using UnityEngine;


namespace Environment
{
    public class Collectable : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            Action(other.gameObject);
        }
        

        protected virtual void Action(GameObject player)
        {
        }
        
    }
}