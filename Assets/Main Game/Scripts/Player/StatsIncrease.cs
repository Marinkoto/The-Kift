using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsIncrease : MonoBehaviour
{
    public GameObject[] enemies;
    public EnemyHealth[] enemiesHealth;
    public PlayerHealthBar playerHealth;
    public PlayerShoot playerShoot;
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
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }
    }
    public void IncreaseHealth()
    {
        Mathf.RoundToInt(PlayerStats.instance.playerMaxHealth += PlayerStats.instance.playerMaxHealth *= 0.1f);
        playerHealth.SetUIHealth(PlayerStats.instance.playerHealth, PlayerStats.instance.playerMaxHealth);
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }

    }
    public void IncreaseMoveSpeed()
    {
        PlayerStats.instance.playerMoveSpeed+=PlayerStats.instance.playerMoveSpeed *= 0.1f;
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }
    }
    public void AddHp()
    {
        Mathf.RoundToInt(PlayerStats.instance.playerHealth += PlayerStats.instance.playerHealth + 20);
        playerHealth.SetUIHealth(PlayerStats.instance.playerHealth, PlayerStats.instance.playerMaxHealth);
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }
    }
    public void InstaLevelUp()
    {
        PlayerStats.instance.LevelUp();
        
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }
    }
    public void DecreaseDashCoolDown()
    {
        if (!PlayerStats.instance.canDash)
        {
            PlayerStats.instance.playerMoveSpeed += PlayerStats.instance.playerMoveSpeed *= 0.1f;
        }
        PlayerStats.instance.dashCooldown = PlayerStats.instance.dashCooldown - 0.05f;
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }
    }
    public void IncreaseBulletsShot()
    {
        if (PlayerStats.instance.hasAxe)
        {
            Mathf.RoundToInt(PlayerStats.instance.playerDamage += PlayerStats.instance.playerDamage *= 0.075f);
        }
        PlayerStats.instance.playerDamage -= PlayerStats.instance.playerDamage * 0.25f;
        PlayerStats.instance.bulletAmount = PlayerStats.instance.bulletAmount + 1;
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }
    }
    public void DecreaseReloadTime()
    {
        playerShoot = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
        playerShoot.classReloadTime = playerShoot.classReloadTime - 0.05f;
        PlayerStats.instance.reloadTime = PlayerStats.instance.reloadTime - 0.05f;
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }
    }
    public void IncreaseClipSize()
    {
        if (PlayerStats.instance.hasAxe)
        {
            Mathf.RoundToInt(PlayerStats.instance.playerDamage += PlayerStats.instance.playerDamage *= 0.075f);
        }
        PlayerStats.instance.currentClip = PlayerStats.instance.currentClip + 1;
        PlayerStats.instance.maxClipSize = PlayerStats.instance.maxClipSize + 1;
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }
    }
    public void AddExp()
    {
        PlayerStats.instance.currentExp = PlayerStats.instance.currentExp + 50;
        PlayerStats.instance.SetExpBar();
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }
    }
    public void StartTime()
    {
        Time.timeScale = 1f;
    }
}
