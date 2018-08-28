using UnityEngine;

namespace BGJ2018
{
    public class CameraController : MonoBehaviour
    {
        public Transform Target;
        public float SmoothTime;

        private Vector3 velocity;

        private void FixedUpdate()
        {
            Vector3 newPos = Vector3.SmoothDamp(transform.position, Target.transform.position, ref velocity, SmoothTime);
            transform.position = newPos;
        }
    }
}

