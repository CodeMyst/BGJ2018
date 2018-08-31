using System.Collections;
using TMPro;
using UnityEngine;

namespace BGJ2018
{
    public class Intro : MonoBehaviour
    {
        [SerializeField]
        [Multiline]
        private string [] intro;

        private float characterTime = 0.025f;
        private float paragraphTime = 3f;
        [SerializeField]
        private TextMeshProUGUI textUI;

        private void Start ()
        {
            StartCoroutine (DisplayIntro ());
        }

        private IEnumerator DisplayIntro ()
        {
            foreach (string p in intro)
            {
                textUI.text = "";

                int index = 0;

                do
                {
                    textUI.text += p [index];
                    index++;
                    yield return new WaitForSeconds (characterTime);
                } while (index + 1 <= p.Length);

                yield return new WaitForSeconds (paragraphTime);
            }

            FindObjectOfType<SceneLoader> ().LoadNextScene ();
        }
    }
}