using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Dialogue
{
    public string dialogueID;
    [TextArea(3, 10)]
    public string[] dialogueLines;
    public string[] playerOptions;
    public Dialogue[] nextDialogues; // Corresponding next dialogues based on player choice
    public bool isCompleted;
}
public class StoryManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public Dialogue dialogue;
    public Button[] optionButtons;
    private Dialogue currentDialogue;
    public List<Dialogue> allDialogues;
    private int currentLine = 0;
    private List<Dialogue> availableDialogues = new List<Dialogue>();
    private const string ProgressKey = "StoryProgress";
    private const string CompletedDialoguesKey = "CompletedDialogues";
    public bool test;
    private bool isAnimating;

    void Start()
    {
        if (test)
        {
            PlayerPrefs.DeleteKey(CompletedDialoguesKey);
        }
        dialoguePanel.SetActive(false);
        allDialogues.Add(dialogue);
        LoadProgress();
        StartDialogue(availableDialogues[0]);
    }
    public void StartDialogue(Dialogue dialogue)
    {
        // Check if the dialogue is marked as completed
        if (dialogue.isCompleted)
        {
            // Skip the dialogue if it's already completed
            return;
        }

        currentDialogue = dialogue;
        currentLine = 0;
        DisplayDialogue();
    }


    void DisplayDialogue()
    {
        dialoguePanel.SetActive(true);

        if (currentLine < currentDialogue.dialogueLines.Length)
        {
            dialogueText.text = currentDialogue.dialogueLines[currentLine];

            if (currentLine < currentDialogue.dialogueLines.Length - 1)
            {
                // Wait for a short duration before displaying the next sentence
                StartCoroutine(DisplayTextWithTypewriterEffect());
            }
            else
            {
                // Display options when all sentences are shown
                DisplayOptions();
            }

            currentLine++; // Move to the next sentence
        }
    }
    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    void DisplayOptions()
    {
        for (int i = 0; i < currentDialogue.playerOptions.Length; i++)
        {
            optionButtons[i].gameObject.SetActive(true);
            optionButtons[i].GetComponentInChildren<TMP_Text>().text = currentDialogue.playerOptions[i];

            // Attach click listener to the option button
            optionButtons[i].onClick.RemoveAllListeners(); // Clear previous listeners
            optionButtons[i].onClick.AddListener(() => ChooseOption());
        }

        // Disable unused buttons
        for (int i = currentDialogue.playerOptions.Length; i < optionButtons.Length; i++)
        {
            optionButtons[i].gameObject.SetActive(false);
        }
    }

    public void ChooseOption()
    {
        if (currentDialogue.nextDialogues.Length > 0)
        {
            currentDialogue = currentDialogue.nextDialogues[0];
            currentLine = 0;
            foreach (Button button in optionButtons)
            {
                button.gameObject.SetActive(false);
            }
            // Display the next line of dialogue or options
            DisplayDialogue();

            // Save progress after choosing an option
            SaveProgress();
        }
        else
        {
            EndDialogue();
            Debug.LogError($"No next dialogues available for the current option.");
        }
    }

    IEnumerator DisplayTextWithTypewriterEffect()
    {
        isAnimating = true;

        for (int i = currentLine; i < currentDialogue.dialogueLines.Length; i++)
        {
            string dialogueLine = currentDialogue.dialogueLines[i];

            dialogueText.text = ""; // Clear the text before starting the effect

            float typeSpeed = 0.000000000001f; // Adjust the typing speed as needed

            for (int j = 0; j < dialogueLine.Length; j++)
            {
                dialogueText.text += dialogueLine[j];
                yield return new WaitForSeconds(typeSpeed);
            }

            // Optionally, introduce a delay between sentences
            yield return new WaitForSeconds(0f); // Adjust the delay as needed
        }

        isAnimating = false;

        // Display options when all sentences are shown
        DisplayOptions();
    }

    void LoadProgress()
    {
        string savedProgress = PlayerPrefs.GetString(ProgressKey, "");
        string[] completedDialogues = PlayerPrefs.GetString(CompletedDialoguesKey, "").Split(',');

        foreach (Dialogue dialogue in allDialogues)
        {
            // Skip completed dialogues when building the list of available dialogues
            if (!IsDialogueCompleted(dialogue.dialogueID, completedDialogues))
            {
                availableDialogues.Add(dialogue);
            }

            if (dialogue.dialogueID == savedProgress && !IsDialogueCompleted(dialogue.dialogueID, completedDialogues))
            {
                currentDialogue = dialogue;
                currentLine = 0;
                DisplayDialogue();
                return;
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveProgress();
    }
    void SaveProgress()
    {
        PlayerPrefs.SetString(ProgressKey, currentDialogue.dialogueID);

        // Mark the current dialogue as completed and save it
        string completedDialogues = PlayerPrefs.GetString(CompletedDialoguesKey, "");
        completedDialogues += currentDialogue.dialogueID + ",";
        PlayerPrefs.SetString(CompletedDialoguesKey, completedDialogues);
        availableDialogues.Remove(currentDialogue);
        PlayerPrefs.Save();
    }

    bool IsDialogueCompleted(string dialogueID, string[] completedDialogues)
    {
        return System.Array.Exists(completedDialogues, id => id == dialogueID);
    }

}
