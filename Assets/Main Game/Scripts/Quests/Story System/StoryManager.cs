using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public Button[] optionButtons;
    public Dialogue dialogue1;
    public bool dialogueEnded;
    public Image icon;

    private Queue<Dialogue.DialogueLine> dialogueQueue;
    private string saveKey;
    private void Start()
    {
        // Check if the dialogue has been completed before starting
        if (!IsDialogueCompleted(dialogue1))
        {
            StartDialogue(dialogue1);
        }
        else
        {
            // Dialogue has been completed, you can handle this situation as needed
            Debug.Log("Dialogue already completed.");
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueEnded = false;
        icon.sprite = dialogue.icon;
        PlayerStats.instance.GetComponent<PlayerMovement>().enabled = false;
        PlayerStats.instance.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        PlayerStats.instance.GetComponent<PlayerShoot>().enabled = false;
        PlayerStats.instance.GetComponent<PlayerHealth>().enabled = false;
        dialogueQueue = new Queue<Dialogue.DialogueLine>(dialogue.dialogueLines);
        dialoguePanel.SetActive(true);
        DisplayNextLine();

    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue.DialogueLine currentLine = dialogueQueue.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeDialogue(currentLine));
    }

    IEnumerator TypeDialogue(Dialogue.DialogueLine line)
    {
        nameText.text = line.speaker;
        dialogueText.text = "";

        float typingSpeed = 0.03f; // Adjust the typing speed as needed

        foreach (char letter in line.text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (line.options != null && line.options.Count > 0)
        {
            DisplayOptions(line.options);
        }
        else
        {
            yield return new WaitForSeconds(6); // Adjust as needed
            DisplayNextLine();
        }
    }

    void DisplayOptions(List<Dialogue.DialogueOption> options)
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < options.Count)
            {
                optionButtons[i].gameObject.SetActive(true);
                int optionIndex = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnOptionSelected(options[optionIndex].nextLineIdentifier));
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = options[optionIndex].optionText;
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnOptionSelected(string nextLineIdentifier)
    {
        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }

        // Find the corresponding dialogue line based on the nextLineIdentifier
        Dialogue.DialogueLine nextLine = dialogueQueue.FirstOrDefault(line => line.options != null && line.options.Any(option => option.nextLineIdentifier == nextLineIdentifier));
        DisplayNextLine();
    }

    public void EndDialogue()
    {
        dialogueEnded = true;
        dialoguePanel.SetActive(false);
        PlayerStats.instance.GetComponent<PlayerMovement>().enabled = true;
        PlayerStats.instance.GetComponent<PlayerShoot>().enabled = true;
        PlayerStats.instance.GetComponent<PlayerHealth>().enabled = true;
        PlayerStats.instance.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        // Save that the dialogue has been completed
        PlayerPrefs.SetInt(saveKey, 1);
        PlayerPrefs.Save();

        // Additional logic for ending dialogue
    }

    public bool IsDialogueCompleted(Dialogue dialogue)
    {
        saveKey = "DialogueCompleted_" + dialogue.name;
        // Check if the dialogue has been completed by looking at the saved data
        return PlayerPrefs.GetInt(saveKey, 0) == 1;
    }
}