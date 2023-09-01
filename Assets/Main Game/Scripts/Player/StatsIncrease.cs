using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsIncrease : MonoBehaviour
{
    public GameObject[] enemies;
    public EnemyHealth[] enemiesHealth;
    public void IncreaseDamage()
    {
        Mathf.RoundToInt(PlayerStats.instance.playerDamage+= PlayerStats.instance.playerDamage *= 0.03f);
        foreach (var enemy in enemiesHealth)
        {
            enemy.health *= 0.05f;
        }
    }
    public void IncreaseHealth()
    {
        Mathf.RoundToInt(PlayerStats.instance.playerMaxHealth += PlayerStats.instance.playerMaxHealth *= 0.03f);
        foreach (var enemy in enemiesHealth)
        {
            enemy.health *= 0.03f;
        }

    }
    public void IncreaseMoveSpeed()
    {
        PlayerStats.instance.playerMoveSpeed+=PlayerStats.instance.playerMoveSpeed *= 0.03f;
        foreach (var enemy in enemiesHealth)
        {
            enemy.health *= 0.04f;
        }
    }
    public void StartTime()
    {
        Time.timeScale = 1f;
    }
}
