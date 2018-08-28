using UnityEngine;

namespace BGJ2018
{
    public class LevelTrigger : MonoBehaviour
    {
        private LevelManager levelManager;

        private void Start()
        {
            levelManager = FindObjectOfType<LevelManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerController>();

            if (player == null) return;
            player.LatestCheckpoint = transform.position;
            levelManager.LevelUp();
            gameObject.SetActive(false);
        }
    }
}
