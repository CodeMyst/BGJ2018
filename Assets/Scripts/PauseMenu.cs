using UnityEngine;

namespace BGJ2018
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject pauseMenuUI;
        [SerializeField]
        private SceneLoader sceneLoader;

        private bool paused = false;

        private void Update ()
        {
            if (Input.GetKeyDown (KeyCode.Escape))
                Pause ();
        }

        public void Pause ()
        {
            paused = !paused;
            pauseMenuUI.SetActive (paused);
            Time.timeScale = paused ? 0 : 1;
        }

        public void ExitToDesktop ()
        {
            sceneLoader.ExitGame ();
        }
    }
}