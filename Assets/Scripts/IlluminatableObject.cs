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
        [SerializeField]
        private Color materialColor;

        [SerializeField]
        private GameObject illuminatable;
        private Renderer r;

        [SerializeField]
        private float maxLightIntensity = 3.5f;

        public bool Illuminated { get; protected set; }

        public UnityEvent OnIlluminated;

        [SerializeField]
        private float energyRequired = 20;
        public float EnergyRequired => energyRequired;
        private float energy;

        public bool MaxEnergy => energy == energyRequired;

        private void Start ()
            => r = illuminatable.GetComponent<Renderer> ();

        public void AddEnergy (float energy)
        {
            IncrementEnergyValue(energy);
            if (Illuminated == false && energy > 0f)
            {
                light.enabled = true;
                meshRenderer.material = onMaterial;
                Illuminated = true;
            }

            UpdateLight();
        }

        private void IncrementEnergyValue(float energyToAdd)
        {
            if (energy > energyRequired) return;

            this.energy += energyToAdd;

            if (energy >= energyRequired)
            {
                OnIlluminated.Invoke();
            }
        }

        private void UpdateLight ()
        {
            float percentLit = (energy / energyRequired) * 100f;
            if (percentLit >= 125) return;
            float intensity = (maxLightIntensity * percentLit) / 100f;
            r.material.SetColor ("_EmissionColor", materialColor * intensity * 3.5f);
            light.intensity = intensity;
        }
    }
}