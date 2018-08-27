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
            audioSource.volume = 0;
            if (player == null) return;
            range = player.hearingRange;
        }

        internal void PlayNewClip(SoundEffectInfo soundEffect, bool randomizePitch = false)
        {
            if (soundEffect == null) return;

            currentSoundEffect = soundEffect;
            audioSource.clip = currentSoundEffect.Clip;
            audioSource.pitch = randomizePitch ? Random.Range(0.975f, 1.025f) : 1;
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
                : Get2DVolume();

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

        private float Get2DVolume()
        {
            return player == null ? 0.5f : player.volume / 100;
        }
    }
}
