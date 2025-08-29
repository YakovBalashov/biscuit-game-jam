using UnityEngine;

namespace Environment
{
    public class BackAndForthMovement : MonoBehaviour
    {
        [SerializeField] private Transform pointA;
        [SerializeField] private Transform pointB;
        [SerializeField] private float speed;

        private void Update()
        {
            var pingPong = Mathf.PingPong(Time.time * speed, 1);
            transform.position = Vector3.Lerp(pointA.position, pointB.position, pingPong);
        }
    }
}
