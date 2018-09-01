using UnityEngine;

namespace BGJ2018
{
    public class EndGameTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneLoader.Instance.LoadNextScene();
            }
        }
    }
}
