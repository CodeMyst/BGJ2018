using UnityEngine;

namespace BGJ2018.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffect : MonoBehaviour
    {
        [SerializeField] private bool soundEffectIs3D;

        private PlayerController player;

        private SoundEffectInfo currentSoundEffect;
        private AudioSource audioSource;

        private float range;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            var playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj == null)
            {
                Debug.LogWarning("Could not find player in the scene. Are you sure they have the 'Player' tag?");
                return;
            }

            player = playerObj.GetComponent<PlayerController>();
        }

        private void Start()
        {
            range = player.hearingRange;
            audioSource.volume = 0;
        }

        internal void PlayNewClip(SoundEffectInfo soundEffect)
        {
            if (soundEffect == null) return;

            currentSoundEffect = soundEffect;
            audioSource.clip = currentSoundEffect.Clip;
            audioSource.Play();
        }

	    private void Update ()
	    {
            SetNewVolume();
	    }

        private void SetNewVolume()
        {
            audioSource.volume = soundEffectIs3D
                ? Get3DVolume()
                : player.volume / 100;

            if (currentSoundEffect != null)
            {
                audioSource.volume *= currentSoundEffect.Volume;
            }
        }

        private float Get3DVolume()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance > range)
            {
                return 0;
            }

            return (1 - distance / range) * (player.volume / 100);
        }
    }
}
