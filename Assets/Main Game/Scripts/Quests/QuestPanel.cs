using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class QuestPanel : MonoBehaviour
{
    public GameObject questBlockPrefab;
    public Transform questBlockContainer;
    public QuestManager questManager;
    public DungeonInfo dungeonInfo;
    public TextMeshProUGUI timerText;
    void OnEnable()
    {
        DisplayQuests();
    }

    void DisplayQuests()
    {
        // Clear existing quest blocks
        foreach (Transform child in questBlockContainer)
        {
            if (child.gameObject.CompareTag("Quest"))
            {
                Destroy(child.gameObject);
            }
        }

        // Display each quest in a separate block
        foreach (Quest quest in questManager.quests)
        {
            // Instantiate a new quest block prefab
            GameObject questBlock = Instantiate(questBlockPrefab, questBlockContainer);
            if (questManager.quests.Count <= 0)
            {
                timerText.gameObject.SetActive(true);
            }
            else
            {
                timerText.gameObject.SetActive(false);
            }
            // Set quest details in the block
            TextMeshProUGUI questTitleText = questBlock.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI questDescriptionText = questBlock.transform.Find("Description").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI questRewardsText = questBlock.transform.Find("Rewards").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI progressRewardsText = questBlock.transform.Find("Progress").GetComponent<TextMeshProUGUI>();


            questTitleText.text = quest.name;
            questDescriptionText.text = quest.description;
            questRewardsText.text = quest.reward.ToString();
            if (quest.questType.ToString() == "EnemyKill")
            {
                progressRewardsText.text = "Progress: " + quest.kills.ToString() + "/" + quest.killsRequired;
            }
            else if (quest.questType.ToString() == "BossKill")
            {
                progressRewardsText.text ="Progress: " + quest.bossKills.ToString() + "/" + quest.bossKillsRequired;
            }
            else if (quest.questType.ToString() == "Reach")
            {
                progressRewardsText.text = "Progress: " + dungeonInfo.dungeonLevel.ToString() + "/" + quest.goal;
            }
            else if (quest.questType.ToString() == "Collect")
            {
                progressRewardsText.text = "Progress: " + PlayerStats.instance.currentExp + "/" + quest.expGoal;
            }
            else if (quest.questType.ToString() == "Level")
            {
                progressRewardsText.text = "Progress: " + PlayerStats.instance.level + "/" + quest.levelGoal;
            }
            else
            {
                progressRewardsText.text = "";
            }
        }
    }
}
