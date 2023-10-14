using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsIncrease : MonoBehaviour
{
    public GameObject[] enemies;
    public EnemyHealth[] enemiesHealth;
    public PlayerHealthBar playerHealth;
    private void Start()
    {
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth = enemy.boundHealth;
            enemy.health = enemy.boundHealth;
        }
    }
    public void IncreaseDamage()
    {
        Mathf.RoundToInt(PlayerStats.instance.playerDamage+= PlayerStats.instance.playerDamage *= 0.075f);
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.05f;
        }
    }
    public void IncreaseHealth()
    {
        Mathf.RoundToInt(PlayerStats.instance.playerMaxHealth += PlayerStats.instance.playerMaxHealth *= 0.1f);
        playerHealth.SetUIHealth(PlayerStats.instance.playerHealth, PlayerStats.instance.playerMaxHealth);
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.05f;
        }

    }
    public void IncreaseMoveSpeed()
    {
        PlayerStats.instance.playerMoveSpeed+=PlayerStats.instance.playerMoveSpeed *= 0.1f;
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.05f;
        }
    }
    public void StartTime()
    {
        Time.timeScale = 1f;
    }
}
