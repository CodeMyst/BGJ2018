using UnityEngine;

using BGJ2018.Helpers;
using System.Collections;

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
        [SerializeField]
        private float fireRate = 0.25f;

        private Vector3 lookDirection;

        [SerializeField]
        private float maxEnergy = 100f;
        private float energy;

        private void Start ()
        {
            energy = maxEnergy;
        }

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
            Debug.DrawRay(r.origin, r.direction * 100);
            if (Physics.Raycast (r, out hit))
            {
                IlluminatableObject i = hit.transform.gameObject.GetComponent<IlluminatableObject> ();
                if (i != null)
                {
                    aimGuide.material = aimGuideMaterialOn; 

                    if (Input.GetMouseButtonDown (0))
                        StartCoroutine (ShootAtIlluminatable (i, 5f));
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

        private IEnumerator ShootAtIlluminatable (IlluminatableObject i, float amountOfEnergy)
        {
            if (energy <= 0 || i.MaxEnergy) yield break;
            float energyAdded = 0;
            do
            {
                energy -= fireRate;
                i.AddEnergy (fireRate);
                energy += fireRate;
                yield return new WaitForEndOfFrame ();
            } while (energyAdded < amountOfEnergy && Input.GetMouseButton (0));
        }
    }
}