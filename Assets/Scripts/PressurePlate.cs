using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace BGJ2018
{
    public class PressurePlate : MonoBehaviour
    {
        [SerializeField]
        private float minY;
        [SerializeField]
        private float maxY;
        [SerializeField]
        private float speed;

        public UnityEvent OnStepped;

        private void OnTriggerEnter (Collider c)
        {
            if (c.tag != "Player") return;

            StartCoroutine (Lower ());
        }

        private void OnTriggerExit (Collider c)
        {
            if (c.tag != "Player") return;

            StartCoroutine (Lift ());
        }

        private IEnumerator Lower ()
        {
            do
            {
                transform.position -= new Vector3 (0, speed, 0);
                yield return new WaitForEndOfFrame ();
            } while (transform.position.y > minY);
            OnStepped?.Invoke ();
        }

        private IEnumerator Lift  ()
        {
            do
            {
                transform.position += new Vector3 (0, speed, 0);
                yield return new WaitForEndOfFrame ();
            } while (transform.position.y < maxY);
        }
    }
}