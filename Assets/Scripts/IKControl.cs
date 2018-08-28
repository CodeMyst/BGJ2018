using UnityEngine;

namespace BGJ2018
{
    [RequireComponent (typeof (Animator))]
    public class IKControl : MonoBehaviour
    {
        protected Animator Animator;
        public Transform RightHandObject;
        public Transform LookObject;

        private void Start ()
        {
            Animator = GetComponent<Animator> ();
        }

        private void OnAnimatorIK ()
        {
            if (LookObject != null)
            {
                Animator.SetLookAtWeight (1);
                Animator.SetLookAtPosition (LookObject.position);
            }

            if (RightHandObject != null)
            {
                Animator.SetIKPositionWeight (AvatarIKGoal.RightHand, 1);
                Animator.SetIKRotationWeight (AvatarIKGoal.RightHand, 1);
                Animator.SetIKPosition (AvatarIKGoal.RightHand, RightHandObject.position);
                Animator.SetIKPosition (AvatarIKGoal.RightHand, RightHandObject.position);
            }
        }
    }
}