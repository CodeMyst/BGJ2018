using UnityEngine;

using BGJ2018.Helpers;

namespace BGJ2018
{
    public class LightGun : MonoBehaviour
    {
        private Vector3 rotatePoint;
        [SerializeField]
        private Transform firePoint;
        [SerializeField]
        private LineRenderer aimGuide;
        [SerializeField]
        private Material aimGuideMaterial;
        [SerializeField]
        private Material aimGuideMaterialOn;

        private Vector3 lookDirection;

        private void Update ()
        {
            Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            Vector3 hitpos = Vector3.zero;
            if (Physics.Raycast (r, out hit))
                hitpos = hit.point;

            lookDirection = hitpos - transform.position;
            lookDirection.y = 0;
            transform.LookAt (transform.position + lookDirection, Vector3.up);

            transform.localEulerAngles = new Vector3 (0, AngleHelper.ClampAngle (transform.localEulerAngles.y, -30, 30), transform.localEulerAngles.z);

            HandleRay ();
        }

        private void HandleRay ()
        {
            RaycastHit hit;
            Ray r = new Ray (firePoint.position, lookDirection.normalized);
            if (Physics.Raycast (r, out hit))
            {
                IlluminatableObject i = hit.transform.gameObject.GetComponent<IlluminatableObject> ();
                if (i != null)
                {
                    aimGuide.material = aimGuideMaterialOn; 

                    if (Input.GetMouseButtonDown (0))
                        ShootAtIlluminatable (i);
                }
                else
                    aimGuide.material = aimGuideMaterial;
            }
            else
            {
                if (aimGuide.material != aimGuideMaterial)
                    aimGuide.material = aimGuideMaterial;
            }
        }

        private void ShootAtIlluminatable (IlluminatableObject i)
        {
            i.Illuminate ();
        }
    }
}