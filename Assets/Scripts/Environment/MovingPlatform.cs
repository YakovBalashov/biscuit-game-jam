using Player;
using UnityEngine;

namespace Environment
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Transform pointA;
        [SerializeField] private Transform pointB;
        [SerializeField] private float speed;

        private void Update()
        {
            var pingPong = Mathf.PingPong(Time.time * speed, 1);
            transform.parent.position = Vector3.Lerp(pointA.position, pointB.position, pingPong);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<PlayerMovement>() == null) return;
            other.transform.SetParent(transform.parent);
        }
        
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.GetComponent<PlayerMovement>() == null) return;
            other.transform.SetParent(null);
        }
    }
}
