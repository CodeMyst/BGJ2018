using System;
using UnityEngine;

namespace BGJ2018
{
    [CreateAssetMenu]
    public class DialogueAsset : ScriptableObject
    {
        public CharacterDialogue [] Dialogue;
    }

    [Serializable]
    public class CharacterDialogue
    {
        public string Name;
        [Multiline]
        public string Text;
    }
}