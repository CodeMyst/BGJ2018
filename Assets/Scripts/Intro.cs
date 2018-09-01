using System.Collections;
using TMPro;
using UnityEngine;

namespace BGJ2018
{
    public class Intro : MonoBehaviour
    {
        [SerializeField] [TextArea(1,5)] private string [] intro;
        [SerializeField] private TextMeshProUGUI textUI;

        private int paragraphIndex;
        private float characterTime = 0.025f;
        private Coroutine writingCoroutine;

        private void Start ()
        {
            writingCoroutine = StartCoroutine(WriteParagraph(intro[paragraphIndex]));
        }

        private void Update()
        {
            var pressedSkipButton = Input.GetMouseButtonDown(0) 
                                || Input.GetKeyDown(KeyCode.Space)
                                || Input.GetKeyDown(KeyCode.E);

            if (!pressedSkipButton) return;

            if (paragraphIndex > intro.Length -1)
            {
                if (SceneLoader.Instance.GetActiveSceneIndex() == 1)
                {
                    SceneLoader.Instance.LoadNextScene();
                    return;
                }

                if (SceneLoader.Instance.GetActiveSceneIndex() == 3)
                {
                    Application.Quit();
                    return;
                }
            }

            if (writingCoroutine == null)
            {
                writingCoroutine = StartCoroutine(WriteParagraph(intro[paragraphIndex]));
                pressedSkipButton = false;
            }
            else
            {
                StopCoroutine(writingCoroutine);
                textUI.text = intro[paragraphIndex];
                writingCoroutine = null;
                pressedSkipButton = false;
                paragraphIndex++;
            }
        }

        private IEnumerator WriteParagraph(string paragraph)
        {
            textUI.text = "";
            int charIndex = 0;

            do
            {
                textUI.text += paragraph[charIndex];
                charIndex++;
                yield return new WaitForSeconds(characterTime);
            }
            while (charIndex + 1 <= paragraph.Length);

            paragraphIndex++;
            writingCoroutine = null;
            //yield return new WaitUntil(() => pressedSkipButton);
        }

        /*
        private IEnumerator DisplayIntro ()
        {
            foreach (string p in intro)
            {

                int charIndex = 0;

                
                //yield return new WaitForSeconds (paragraphTime);
            }

        }
        */
    }
}