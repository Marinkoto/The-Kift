using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public StoryManager storyManager;
    private void Start()
    {
        storyManager = GameObject.Find("Story Manager").GetComponent<StoryManager>();
        if (!storyManager.IsDialogueCompleted(dialogue))
        {
            FindObjectOfType<StoryManager>().StartDialogue(dialogue);
        }
        else
        {
            // Dialogue has been completed, you can handle this situation as needed
            Debug.Log("Dialogue already completed.");
        }
    }
}
