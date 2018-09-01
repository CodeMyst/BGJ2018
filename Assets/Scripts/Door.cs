using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 LocalTargetPosition;
    public float OpeningTime;

    private Vector3 startPosition;
    private Vector3 globalTargetPosition;
    private Vector3 velocity;

    private bool open;

    private void Start()
    {
        startPosition = transform.position;
        globalTargetPosition = startPosition + LocalTargetPosition;
    }

    private void Update()
    {
        if (open)
        {
            transform.position = Vector3.SmoothDamp(transform.position, globalTargetPosition, ref velocity, OpeningTime);
        }
    }

    public void Reset ()
    {
        open = false;
        transform.position = startPosition;
    }

    public void Open()
    {
        open = true;
    }
}
