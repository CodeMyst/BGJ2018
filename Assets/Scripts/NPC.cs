using UnityEngine;

namespace BGJ2018
{
    public class NPC : MonoBehaviour
    {
        [SerializeField]
        private DialogueAsset dialogue;
        [SerializeField]
        private DialogueManager dialogueManager;

        private void OnTriggerEnter (Collider c)
        {
            if (c.tag != "Player") return;

            dialogueManager.DisplayText (dialogue);
        }

        private void OnTriggerExit (Collider c)
        {
            if (c.tag != "Player") return;

            dialogueManager.Reset ();
        }
    }
}