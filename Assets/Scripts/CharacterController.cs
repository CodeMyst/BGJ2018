using UnityEngine;

namespace BGJ2018
{
    [RequireComponent (typeof (Rigidbody))]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 12f;
        [SerializeField]
        private float rotationSpeed = 2;
        private float rotationDeadzone = 0.2f;

        private Rigidbody rb;

        private Vector3 input;

        private void Start ()
        {
            rb = GetComponent<Rigidbody> ();
        }

        private void Update ()
        {
            HandleInput ();
            Turn ();
        }

        private void Turn ()
        {
            if (rb.velocity.magnitude != 0 && Mathf.Abs (input.x) >= rotationDeadzone || Mathf.Abs (input.z) >= rotationDeadzone)
            {
                Quaternion lookRotation = Quaternion.LookRotation (rb.velocity.normalized);
                transform.rotation = Quaternion.Lerp (transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }

        private void HandleInput ()
        {
            input = new Vector3
            (
                Input.GetAxis ("Horizontal"),
                0f,
                Input.GetAxis ("Vertical")
            );
        }

        private void FixedUpdate ()
        {
            Move ();
        }

        private void Move ()
        {
            rb.velocity = input * speed;
        }
    }
}