using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public BossHealth bossHealth;
    public Slider healthBar;
    public TextMeshProUGUI healthCounter;
    public TextMeshProUGUI bossName;
    public GameObject dungeonInfoUI;
    private void Start()
    {
        dungeonInfoUI = GameObject.Find("Dungeon Counter");
    }
    private void Update()
    {
        SetHealthBar();
    }
    public void SetHealthBar()
    {
        healthBar.value = bossHealth.health;
        healthBar.maxValue = bossHealth.maxHealth;
        healthCounter.text = Mathf.RoundToInt(healthBar.value) + "/" + Mathf.RoundToInt(healthBar.maxValue);
    }
    public void SetBossName(string name)
    {
        bossName.text = name;
    }
    public void DisableBar()
    {
        healthBar.gameObject.SetActive(false);
        healthCounter.gameObject.SetActive(false);
        bossName.gameObject.SetActive(false);
    }
}
