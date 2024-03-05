using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public struct DialogueLine
    {
        public string speaker;
        public string text;
        public List<DialogueOption> options;
    }

    [System.Serializable]
    public struct DialogueOption
    {
        public string optionText;
        public string nextLineIdentifier; // Identifier for the next line to be displayed
    }
    public Sprite icon;
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
