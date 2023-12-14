using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
[Serializable]
public class QuestData
{
    public HashSet<string> completedQuests = new HashSet<string>();
}
public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public QuestData questData = new QuestData();
    public static QuestManager instance;
    private DateTime lastLoginDate;
    public DungeonInfo dungeonInfo;
    public TextMeshProUGUI questText;
    public Animator questAnimator;
    public TextMeshProUGUI timerText;
    public float dailyRefreshInterval = 24f; // Daily refresh interval in hours

    private TimeSpan timeUntilRefresh;

    void CalculateTimeUntilRefresh()
    {
        // Calculate the time until the next daily refresh
        DateTime nextRefreshDate = lastLoginDate.AddDays(1);
        timeUntilRefresh = nextRefreshDate - DateTime.Now;
    }

    void UpdateTimerText()
    {
        // Update the timer text to display the time until the next daily refresh
        if (timerText != null)
        {
            timerText.text = $"Next Daily Refresh In: {timeUntilRefresh.Hours:D2}:{timeUntilRefresh.Minutes:D2}:{timeUntilRefresh.Seconds:D2}";
        }
    }
    void UpdateTimer()
    {
        // Update the timer every second
        if (timeUntilRefresh.TotalSeconds > 0)
        {
            timeUntilRefresh = timeUntilRefresh.Subtract(TimeSpan.FromSeconds(1));
            UpdateTimerText();
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    void Start()
    {
        CalculateTimeUntilRefresh();
        UpdateTimerText();
        LoadCompletedQuests(); // Load completed quests data from PlayerPrefs or another storage method
        LoadLastLoginDate();    // Load last login date from PlayerPrefs
        CheckDailyReset();
        DisplayAvailableQuests();
        Debug.Log(lastLoginDate);
        InvokeRepeating(nameof(UpdateTimer), 0f, 1f);
    }

    void OnApplicationQuit()
    {
        SaveCompletedQuests();
        SaveLastLoginDate(lastLoginDate);
    }

    void CheckDailyReset()
    {
        // Get the current date
        DateTime currentDate = DateTime.Now;

        // Check if it's a new day since the last login
        if (currentDate.Date != lastLoginDate.Date)
        {
            // Clear completed quests
            questData.completedQuests.Clear();

            // Randomly select 2 quests from the available quests
            List<Quest> availableQuests = GetAvailableQuests();
            List<Quest> selectedQuests = SelectRandomQuests(availableQuests, 2);

            // Mark selected quests as available
            foreach (Quest quest in selectedQuests)
            {
                questData.completedQuests.Add(quest.name);
            }

            Debug.Log("Daily quest reset! New quests available.");
            SaveCompletedQuests(); // Save completed quests data to PlayerPrefs or another storage method
            lastLoginDate = currentDate; // Update the last login date
            SaveLastLoginDate(lastLoginDate);    // Save last login date to PlayerPrefs
        }
    }
    List<Quest> GetAvailableQuests()
    {
        // Get quests that are not completed
        return quests.FindAll(q => !questData.completedQuests.Contains(q.name));
    }
    List<Quest> SelectRandomQuests(List<Quest> questList, int count)
    {
        // Shuffle the list of available quests
        System.Random rng = new System.Random();
        int n = questList.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Quest value = questList[k];
            questList[k] = questList[n];
            questList[n] = value;
        }

        // Select the first 'count' quests from the shuffled list
        return questList.GetRange(0, Mathf.Min(count, questList.Count));
    }
    void SaveCompletedQuests()
    {
        // Convert the completed quests HashSet to a comma-separated string
        string questsString = string.Join(",", questData.completedQuests.ToArray());
        // Save the completed quests data to PlayerPrefs
        PlayerPrefs.SetString("CompletedQuests", questsString);
        PlayerPrefs.Save();
    }

    void LoadCompletedQuests()
    {
        // Load the completed quests data from PlayerPrefs
        string questsString = PlayerPrefs.GetString("CompletedQuests", "");
        // Split the comma-separated string into an array of quest names
        string[] questNames = questsString.Split(',');
        // Add the quest names to the completedQuests HashSet
        questData.completedQuests = new HashSet<string>(questNames);
    }
    private void SaveLastLoginDate(DateTime currentDate)
    {
        PlayerPrefs.SetString("LastLoginDate", lastLoginDate.ToString("yyyy-MM-dd"));
        PlayerPrefs.Save();
    }

    private void LoadLastLoginDate()
    {
        string lastLoginDateString = PlayerPrefs.GetString("LastLoginDate", "");
        if (!string.IsNullOrEmpty(lastLoginDateString))
        {
            DateTime.TryParseExact(lastLoginDateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out lastLoginDate);
        }
        else
        {
            // Set a default date if it's not available in PlayerPrefs (e.g., first-time launch)
            lastLoginDate = DateTime.Now;
        }
    }

    void DisplayAvailableQuests()
    {
        Debug.Log("Available quests for today:");

        // Remove completed quests from the quests list
        quests.RemoveAll(q => questData.completedQuests.Contains(q.name));

        foreach (Quest quest in quests)
        {
            Debug.Log($"- {quest.name}: {quest.description} (Reward: {quest.reward})");
        }
    }

    public void CompleteQuest(string questName)
    {
        Quest quest = quests.Find(q => q.name == questName);
        if (quest != null)
        {
            questData.completedQuests.Add(questName);
            PlayfabManager.instance.AddCurrency(quest.reward);
            questText.text = $"Quest Completed: '{quest.name}' Rewards: {quest.reward} Cat Tokens";
            quests.Remove(quest);
            StartCoroutine(QuestBlockOpen());
            SaveCompletedQuests();
        }
    }
    public IEnumerator QuestBlockOpen()
    {
        questAnimator.SetBool("isOpen", true);
        yield return new WaitForSeconds(4);
        questAnimator.SetBool("isOpen", false);
    }
    public void EnemyKilled()
    {
        foreach (var quest in quests)
        {
            quest.kills++;
            if (quest.kills >= quest.killsRequired && quest.questType.ToString() == "EnemyKill")
            {
                CompleteQuest(quest.name);
            }
        }
    }
    public void BossKilled()
    {
        foreach (var quest in quests)
        {
            quest.bossKills++;
            if (quest.bossKills >= quest.bossKillsRequired && quest.questType.ToString() == "BossKill")
            {
                CompleteQuest(quest.name);
            }
        }
    }
    public void ReachDungeon()
    {
        foreach (var quest in quests)
        {
            if (dungeonInfo.dungeonLevel == quest.goal && quest.questType.ToString() == "Reach")
            {
                CompleteQuest(quest.name);
            }
        }
    }
    public void CollectEXP()
    {
        foreach(var quest in quests)
        {
            if (PlayerStats.instance.currentExp >= quest.expGoal && quest.questType.ToString() == "Collect")
            {
                CompleteQuest(quest.name);
            }
        }
    }
    public void ReachLevel()
    {
        foreach (var quest in quests)
        {
            if (PlayerStats.instance.level >= quest.levelGoal && quest.questType.ToString() == "Level")
            {
                CompleteQuest(quest.name);
            }
        }
    }
}

