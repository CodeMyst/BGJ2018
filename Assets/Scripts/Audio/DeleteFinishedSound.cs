using UnityEngine;

namespace BGJ2018.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class DeleteFinishedSound : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource.clip == null)
            {
                Debug.LogWarning($"{name} does not have an audioclip. Cannot destroy the object");
                return;
            }

            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
