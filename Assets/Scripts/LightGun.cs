using UnityEngine;

namespace BGJ2018
{
    public class LightGun : MonoBehaviour
    {
        private Vector3 rotatePoint;

        private void Update ()
        {
            Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            Vector3 hitpos = Vector3.zero;
            if (Physics.Raycast (r, out hit))
            {
                hitpos = hit.point;
            }

            Vector3 lookdir = hitpos - transform.position;
            lookdir.y = 0;
            transform.LookAt (transform.position + lookdir, Vector3.up);

            transform.localEulerAngles = new Vector3 (0, ClampAngle (transform.localEulerAngles.y, -30, 30), transform.localEulerAngles.z);
        }

        private float ClampAngle(float angle, float from, float to)
        {
            if (angle < 0f) angle = 360 + angle;
            if (angle > 180f) return Mathf.Max (angle, 360 + from);
            return Mathf.Min (angle, to);
        }
    }
}