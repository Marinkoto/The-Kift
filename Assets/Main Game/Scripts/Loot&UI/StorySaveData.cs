using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class StorySaveData : MonoBehaviour
{
    private const string saveFileName = "dialogue_save.json";
    private const string completedFileName = "completed_dialogue_save.json";

    public void SaveDialogues(List<Dialogue> dialogues)
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        string jsonData = JsonUtility.ToJson(new DialogueListWrapper(dialogues), true);
        File.WriteAllText(filePath, jsonData);
    }

    public List<Dialogue> LoadDialogues()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<DialogueListWrapper>(jsonData).dialogues;
        }
        else
        {
            Debug.LogWarning("Save file not found. Returning an empty list.");
            return new List<Dialogue>();
        }
    }

    public void SaveCompletedDialogues(List<Dialogue> completedDialogues)
    {
        string filePath = Path.Combine(Application.persistentDataPath, completedFileName);

        string jsonData = JsonUtility.ToJson(new DialogueListWrapper(completedDialogues), true);
        File.WriteAllText(filePath, jsonData);
    }

    public List<Dialogue> LoadCompletedDialogues()
    {
        string filePath = Path.Combine(Application.persistentDataPath, completedFileName);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<DialogueListWrapper>(jsonData).dialogues;
        }
        else
        {
            Debug.LogWarning("Completed save file not found. Returning an empty list.");
            return new List<Dialogue>();
        }
    }

    // Wrapper class to serialize/deserialize lists with JsonUtility
    [System.Serializable]
    private class DialogueListWrapper
    {
        public List<Dialogue> dialogues;

        public DialogueListWrapper(List<Dialogue> dialogues)
        {
            this.dialogues = dialogues;
        }
    }
}
