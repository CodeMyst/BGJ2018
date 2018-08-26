using UnityEngine;
using UnityEngine.Events;

namespace BGJ2018
{
    public class IlluminatableObject : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer meshRenderer;
        [SerializeField]
        private Material offMaterial;
        [SerializeField]
        private Material onMaterial;
        [SerializeField]
        private new Light light;

        public bool Illuminated { get; protected set; }

        public UnityEvent OnIlluminated;

        public void Illuminate ()
        {
            if (Illuminated) return;
            meshRenderer.material = onMaterial;
            light.enabled = true;
            Illuminated = true;
            OnIlluminated.Invoke ();
        }

        public void DeIlluminate ()
        {
            if (!Illuminated) return;
            meshRenderer.material = offMaterial;
            light.enabled = false;
            Illuminated = false;
        }
    }
}