using System.Collections;
using UnityEngine;

namespace BGJ2018.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        internal static MusicPlayer Instance { get; private set; }

        [SerializeField] private AudioSource menuSource;
        [SerializeField] private AudioSource choirSource;

        [SerializeField] private AudioSource mainSource;
        [SerializeField] [Range(0, 1)] private float mainSourceMaxVolume;
        [SerializeField] private float mainSourceFadeSpeed;

        private void Awake()
        {
            SingletonCheck();
        }

        private void Start()
        {
            menuSource.Play();
        }

        // Called by the start game button
        public void StartChoirAndMain()
        {
            choirSource.Play();
            StartCoroutine(MainSourceFadeIn());
        }

        private IEnumerator MainSourceFadeIn()
        {
            mainSource.volume = 0;
            mainSource.Play();
            while (mainSource.volume < mainSourceMaxVolume)
            {
                mainSource.volume += mainSourceFadeSpeed / 1000;
                yield return new WaitForEndOfFrame();
            }
        }

        private void SingletonCheck()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
