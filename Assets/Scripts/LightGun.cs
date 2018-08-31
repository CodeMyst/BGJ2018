using UnityEngine;
using BGJ2018.Helpers;
using System.Collections;

namespace BGJ2018
{
    public class LightGun : MonoBehaviour
    {
        [SerializeField] private AudioSource rayActiveSound;
        [SerializeField] private Transform pivotPoint;
        [SerializeField] private float rotateRadius = 3f;
        [SerializeField] private Transform firePoint;
        [SerializeField] private LineRenderer aimGuide;
        [SerializeField] private LineRenderer lightRay;
        [SerializeField] private ParticleSystem shootingParticles;
        [SerializeField] private Material aimGuideMaterial;
        [SerializeField] private Material aimGuideMaterialOn;
        [SerializeField] private float fireRate = 0.25f;
        [SerializeField] private float halfAngleExtent = 30;
        [SerializeField] private float maxEnergy = 100f;

        private float energy;
        public float Energy => energy;
        public float MaxEnergy => maxEnergy;

        private Vector3 lookDirection;
        private int mouseCastLayer;

        private void Start ()
        {
            mouseCastLayer = LayerMask.GetMask("MouseCast");
            lightRay.enabled = false;
            shootingParticles.Pause ();
            ResetEnergy();
        }

        private void Update ()
        {
            if (Time.timeScale == 0) return;

            Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            Vector3 hitpos = Vector3.zero;
            if (Physics.Raycast (r, out hit, Mathf.Infinity, mouseCastLayer))
                hitpos = hit.point;

            lookDirection = hitpos - transform.position;
            lookDirection.y = 0;
            transform.LookAt (transform.position + lookDirection, Vector3.up);

            transform.localEulerAngles = new Vector3 (0, AngleHelper.ClampAngle (transform.localEulerAngles.y, -halfAngleExtent, halfAngleExtent), transform.localEulerAngles.z);

            aimGuide.positionCount = 2;
            aimGuide.SetPosition (0, firePoint.position);
            aimGuide.SetPosition (1, -firePoint.forward * 1000 + transform.position);

            lightRay.positionCount = 2;
            lightRay.SetPosition (0, firePoint.position);
            lightRay.SetPosition (1, -firePoint.forward * 1000 + transform.position);

            if (Input.GetMouseButtonDown (0))
                StartCoroutine (Shoot ());

            HandleRay ();
        }

        // Sets energy to max
        internal void ResetEnergy()
        {
            energy = maxEnergy;
        }

        private IlluminatableObject target;

        private void HandleRay ()
        {
            RaycastHit hit;
            Ray r = new Ray (firePoint.position, lookDirection.normalized);
            Debug.DrawRay(r.origin, r.direction * 100);
            if (Physics.Raycast (r, out hit))
            {
                IlluminatableObject i = hit.transform.gameObject.GetComponent<IlluminatableObject> ();
                if (i != null && Vector3.Angle(transform.forward, lookDirection) < 1)
                {
                    target = i;
                    aimGuide.material = aimGuideMaterialOn; 
                }
                else
                {
                    target = null;
                    aimGuide.material = aimGuideMaterial;
                }
            }
            else
            {
                if (aimGuide.material != aimGuideMaterial)
                    aimGuide.material = aimGuideMaterial;
            }
        }

        private IEnumerator Shoot ()
        {
            if (energy <= 0) yield break;
            lightRay.enabled = true;
            shootingParticles.Play ();
            rayActiveSound.Play();
            aimGuide.enabled = false;
            do
            {
                energy -= fireRate;
                if (target != null)
                {
                    if (target.MaxEnergy == false)
                    {
                        target.AddEnergy (fireRate);
                    }
                }
                yield return new WaitForEndOfFrame ();
            } while (Input.GetMouseButton (0) && energy > 0);
            lightRay.enabled = false;
            shootingParticles.Stop ();
            rayActiveSound.Stop();
            aimGuide.enabled = true;
        }
    }
}