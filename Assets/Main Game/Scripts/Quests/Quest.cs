using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest
{
    public string name;
    public string description;
    public int reward;
    public int killsRequired;
    public int bossKills, bossKillsRequired;
    public int kills;
    public int expGoal;
    public int levelGoal;
    public bool completed;
    public int goal;

    public enum QuestType { BossKill, EnemyKill, Collect, Reach, Level }
    public QuestType questType;
}
