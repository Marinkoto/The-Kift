using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogue : MonoBehaviour
{
    [Header("Dialogues")]
    [SerializeField] Dialogue dialogue;
    [Header("Fields")]
    [SerializeField] StoryManager storyManager;
    [SerializeField] Animator animator;
    [SerializeField] Behaviour[] behaviours;
    Func<bool> dialogueEnded = () => true;

    private void Start()
    {
        storyManager = GameObject.Find("Story Manager").GetComponent<StoryManager>();
        dialogueEnded = () => storyManager.dialogueEnded;
        if (!storyManager.IsDialogueCompleted(dialogue))
        {
            StartDialogue(dialogue);
            StartCoroutine(EndDialogue());
        }
        else
        {
            Debug.Log("Dialogue already completed.");
        }
    }
    void StartDialogue(Dialogue dialogue)
    {
        storyManager.StartDialogue(dialogue);
        animator.enabled = false;
        foreach (var behaviour in behaviours)
        {
            behaviour.enabled = false;
        }
    }
    IEnumerator EndDialogue()
    {
        yield return new WaitUntil(dialogueEnded);
        animator.enabled = true;
        foreach (var behaviour in behaviours)
        {
            behaviour.enabled = true;
        }
    }
}
