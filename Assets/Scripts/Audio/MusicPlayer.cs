using UnityEngine;

namespace BGJ2018.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        internal static MusicPlayer Instance { get; private set; }

        [SerializeField] private AudioClip clip;

        private AudioSource audioSource;

        private void Awake()
        {
            SingletonCheck();
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
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
