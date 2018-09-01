using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BGJ2018
{
    [RequireComponent(typeof(Animator))]
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        internal static SceneLoader Instance { get; private set; }

        private Animator animator;

        private int engageLoadAnimationHash;
        private int disengageLoadAnimationHash;
        private IEnumerator ongoingCoroutine;

        private void Awake()
        {
            SingletonCheck();
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            engageLoadAnimationHash = Animator.StringToHash("EngageLoad");
            disengageLoadAnimationHash = Animator.StringToHash("DisengageLoad");
        }

        internal void ResetLevel(LevelManager levelManager)
        {
            if (ongoingCoroutine != null) return;

            ongoingCoroutine = ResetLevelWithAnimation(levelManager);
            StartCoroutine(ongoingCoroutine);
        }

        private IEnumerator ResetLevelWithAnimation(LevelManager levelManager)
        {
            animator.SetTrigger(engageLoadAnimationHash);

            // Wait for engage animation to completely finish.
            yield return new WaitForSeconds(1);

            levelManager.ResetLevelObjects();

            yield return new WaitForSeconds(1);
            animator.SetTrigger(disengageLoadAnimationHash);

            // Wait for disengage animation to completely finish.
            yield return new WaitForSeconds(1);

            ongoingCoroutine = null;
        }

        private IEnumerator LoadSceneWithAnim(int sceneIndex)
        {
            animator.SetTrigger(engageLoadAnimationHash);

            // Wait for engage animation to completely finish.
            yield return new WaitForSeconds(1); 
            SceneManager.LoadScene(sceneIndex);

            yield return new WaitForSeconds(1);
            animator.SetTrigger(disengageLoadAnimationHash);
        
            // Wait for disengage animation to completely finish.
            yield return new WaitForSeconds(1);

            ongoingCoroutine = null;
        }

        public int GetActiveSceneIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        public void LoadScene(int sceneIndex)
        {
            if (ongoingCoroutine != null) return;

            ongoingCoroutine = LoadSceneWithAnim(sceneIndex);
            StartCoroutine(ongoingCoroutine);
        }

        public void LoadNextScene()
        {
            if (ongoingCoroutine != null) return;

            ongoingCoroutine = LoadSceneWithAnim(SceneManager.GetActiveScene().buildIndex + 1);
            StartCoroutine(ongoingCoroutine);
        }

        public void ExitGame()
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
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
