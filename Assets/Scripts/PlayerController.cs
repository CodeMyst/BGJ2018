using UnityEngine;

namespace BGJ2018
{
    [RequireComponent (typeof (Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        internal Vector3 LatestCheckpoint; // Set by LevelTrigger

        [Header("Movement")]
        [SerializeField] private float speed = 12f;
        [SerializeField] private float rotationSpeed = 2;

        [Header("Hearing")]
        [SerializeField] [Range(0, 100)] internal float volume = 50;
        [SerializeField] internal float hearingRange = 15; // How far away audio sources are when their volume is 0

        private const float RotationDeadzone = 0.2f;

        private Rigidbody rb;
        private Animator anim;

        private Vector3 input;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
            LatestCheckpoint = transform.position;
        }

        private void Update()
        {
            HandleInput();
            Turn();
            Animate();
        }

        private void Turn()
        {
            if (rb.velocity.magnitude != 0 && Mathf.Abs (input.x) >= RotationDeadzone || Mathf.Abs (input.z) >= RotationDeadzone)
            {
                Quaternion lookRotation = Quaternion.LookRotation (rb.velocity.normalized);
                transform.rotation = Quaternion.Lerp (transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }

        private void HandleInput()
        {
            input = new Vector3
            (
                Input.GetAxis("Horizontal"),
                0f,
                Input.GetAxis("Vertical")
            );
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            rb.velocity = input * speed;
        }

        private void Animate()
        {
            anim?.SetBool("Walking", input.sqrMagnitude > Vector3.kEpsilon);
        }
    }
}