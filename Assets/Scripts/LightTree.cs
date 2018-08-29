using BGJ2018.Audio;
using UnityEngine;

namespace BGJ2018
{
    public class LightTree : MonoBehaviour
    {
        [SerializeField] private KeyCode consumeKey = KeyCode.E;

        [SerializeField] private Material onMaterial;
        [SerializeField] private Material offMaterial;

        [SerializeField] private Light light;
        [SerializeField] private MeshRenderer meshRenderer;

        private SoundEffectSpawner soundEffectSpawner;
        private PlayerController playerInsideTrigger;
        private bool consumed;

        private void Start()
        {
            soundEffectSpawner = GetComponent<SoundEffectSpawner>();
            ReplenishTree();
        }

        private void Update()
        {
            if (consumed) return;

            if (Input.GetKeyDown(consumeKey) && playerInsideTrigger)
            {
                ConsumeTree();
            }
        }

        internal void ReplenishTree()
        {
            light.enabled = true;
            meshRenderer.material = onMaterial;
            consumed = false;
        }

        private void ConsumeTree()
        {
            light.enabled = false;
            meshRenderer.material = offMaterial;
            soundEffectSpawner.InstantiateSoundEffect2D("ConsumeTree");
            consumed = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            playerInsideTrigger = other.GetComponent<PlayerController>();
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<PlayerController>();

            if (player == null) return;

            playerInsideTrigger = null;
        }
    }
}
