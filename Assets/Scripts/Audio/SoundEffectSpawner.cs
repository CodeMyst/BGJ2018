using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BGJ2018.Audio
{
    public class SoundEffectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject soundEffectPrefab2D;
        [SerializeField] private GameObject soundEffectPrefab3D;
        [SerializeField] private List<SoundEffectInfo> soundEffects;

        private void Start()
        {
            //InstantiateSoundEffect3D("boom");
        }

        public void InstantiateSoundEffect3D(string soundEffectName)
        {
            var clip = soundEffects.FirstOrDefault(x => x.Name == soundEffectName);

            if (clip == null) return;

            Instantiate(soundEffectPrefab3D, clip.SpawnPosition, Quaternion.identity)
                .GetComponent<SoundEffect>()
                .PlayNewClip(clip);
        }

        public void InstantiateSoundEffect2D(string soundEffectName)
        {
            Instantiate(soundEffectPrefab2D)
                .GetComponent<SoundEffect>()
                .PlayNewClip(soundEffects.FirstOrDefault(x => x.Name == soundEffectName));
        }
    }

    [System.Serializable]
    public class SoundEffectInfo
    {
        public string Name;
        public AudioClip Clip;
        public float Volume;
        public Vector3 SpawnPosition = Vector3.zero;
    }
}
