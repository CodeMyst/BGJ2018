using UnityEngine;
using UnityEngine.Events;

namespace BGJ2018
{
    public class MasterIlluminatableObject : IlluminatableObject
    {
        [SerializeField]
        private IlluminatableObject [] requiredObjects;

        public UnityEvent OnAllIlluminated;

        private bool allIlluminatedRan = false;

        protected override void Start ()
        {
            base.Start ();

            OnIlluminated.AddListener (() => { ObjectIlluminated (); });
            OnAllIlluminated.AddListener (() => { allIlluminatedRan = true; });
            foreach (var obj in requiredObjects)
                obj.OnIlluminated.AddListener (() => { ObjectIlluminated (); });
        }

        private void ObjectIlluminated ()
        {
            if (allIlluminatedRan)
                return;
            if (MaxEnergy == false)
                return;
            foreach (var r in requiredObjects)
                if (r.MaxEnergy == false)
                    return;
        
            OnAllIlluminated?.Invoke ();
        }

        public override void ResetEnergy ()
        {
            base.ResetEnergy ();
            foreach (var obj in requiredObjects)
                obj.ResetEnergy ();
            allIlluminatedRan = false;
        }
    }
}