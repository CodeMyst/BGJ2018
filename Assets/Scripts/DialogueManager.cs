using TMPro;
using UnityEngine;

namespace BGJ2018
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject dialogueUI;
        [SerializeField]
        private TextMeshProUGUI nameText;
        [SerializeField]
        private TextMeshProUGUI text;

        private DialogueAsset currentDialogue;
        private int index = 0;

        private void Start ()
        {
            dialogueUI.SetActive (false);
        }

        private void Update ()
        {
            if (currentDialogue == null) return;

            if (Input.GetKeyDown (KeyCode.Space))
            {
                if (index + 1 > currentDialogue.Dialogue.Length - 1)
                {
                    Reset ();
                    return;
                }
                index++;
                nameText.SetText (currentDialogue.Dialogue [index].Name);
                text.SetText (currentDialogue.Dialogue [index].Text);
            }
        }

        public void DisplayText (DialogueAsset asset)
        {
            currentDialogue = asset;
            index = 0;
            nameText.SetText (asset.Dialogue [index].Name);
            text.SetText (asset.Dialogue [index].Text);
            dialogueUI.SetActive (true);
        }

        public void Reset ()
        {
            dialogueUI.SetActive (false);
            index = 0;
            currentDialogue = null;
        }
    }
}