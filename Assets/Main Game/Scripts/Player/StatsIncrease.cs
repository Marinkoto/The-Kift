using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsIncrease : MonoBehaviour
{
    public GameObject[] enemies;
    public EnemyStats[] enemiesHealth;
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
        IncreaseEnemyHealth();
    }
    public void IncreaseHealth()
    {
        Mathf.RoundToInt(PlayerStats.instance.playerMaxHealth += PlayerStats.instance.playerMaxHealth *= 0.3f);
        playerHealth.SetUIHealth(PlayerStats.instance.playerHealth, PlayerStats.instance.playerMaxHealth);
        IncreaseEnemyHealth();
    }
    public void IncreaseMoveSpeed()
    {
        PlayerStats.instance.playerMoveSpeed+=PlayerStats.instance.playerMoveSpeed *= 0.1f;
        IncreaseEnemyHealth();
    }
    public void AddHp()
    {
        if (PlayerStats.instance.playerHealth <= PlayerStats.instance.playerMaxHealth-50)
        {
            Mathf.RoundToInt(PlayerStats.instance.playerHealth = PlayerStats.instance.playerHealth + 50);
        }
        else
        {
            Mathf.RoundToInt(PlayerStats.instance.playerHealth = PlayerStats.instance.playerMaxHealth);
        }
        playerHealth.SetUIHealth(PlayerStats.instance.playerHealth, PlayerStats.instance.playerMaxHealth);
        IncreaseEnemyHealth();
    }
    public void InstaLevelUp()
    {
        PlayerStats.instance.LevelUp();
        IncreaseEnemyHealth();
    }
    public void DecreaseDashCoolDown()
    {
        if (!PlayerStats.instance.canDash)
        {
            PlayerStats.instance.playerMoveSpeed += PlayerStats.instance.playerMoveSpeed *= 0.1f;
        }
        PlayerStats.instance.dashCooldown = PlayerStats.instance.dashCooldown - 0.05f;
        IncreaseEnemyHealth();
    }
    public void IncreaseBulletsShot()
    {
        if (PlayerStats.instance.hasAxe)
        {
            Mathf.RoundToInt(PlayerStats.instance.playerDamage += PlayerStats.instance.playerDamage *= 0.075f);
        }
        else if (!PlayerStats.instance.hasAxe)
        {
            PlayerStats.instance.playerDamage -= PlayerStats.instance.playerDamage * 0.25f;
            PlayerStats.instance.bulletAmount = PlayerStats.instance.bulletAmount + 1;
        }
        IncreaseEnemyHealth();
    }
    public void DecreaseReloadTime()
    {
        if (PlayerStats.instance.hasAxe)
        {
            Mathf.RoundToInt(PlayerStats.instance.playerDamage += PlayerStats.instance.playerDamage *= 0.075f);
        }
        PlayerStats.instance.reloadTime = PlayerStats.instance.reloadTime - 0.05f;
        IncreaseEnemyHealth();
    }
    public void IncreaseClipSize()
    {
        if (PlayerStats.instance.hasAxe)
        {
            Mathf.RoundToInt(PlayerStats.instance.playerDamage += PlayerStats.instance.playerDamage *= 0.075f);
        }
        PlayerStats.instance.currentClip = PlayerStats.instance.currentClip + 1;
        PlayerStats.instance.maxClipSize = PlayerStats.instance.maxClipSize + 1;
        PlayerStats.instance.currentAmmo = PlayerStats.instance.currentAmmo + 1;
        IncreaseEnemyHealth();
    }
    public void AddExp()
    {
        PlayerStats.instance.currentExp = PlayerStats.instance.currentExp + 150;
        PlayerStats.instance.SetExpBar();
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }
    }
    public void IncreasePickUpRange()
    {
        PlayerStats.instance.pickUpRange = PlayerStats.instance.pickUpRange + 1;
        IncreaseEnemyHealth();
    }
    public void IncreaseEXPMultiplier()
    {
        PlayerStats.instance.expMultiplier = PlayerStats.instance.expMultiplier * 0.25f;
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }
    }
    public void IncreaseArmor()
    {
        PlayerStats.instance.armor = PlayerStats.instance.armor + 0.1f;
        if (PlayerStats.instance.armor>=5f)
        {
            Mathf.RoundToInt(PlayerStats.instance.playerMaxHealth += PlayerStats.instance.playerMaxHealth *= 0.1f);
            playerHealth.SetUIHealth(PlayerStats.instance.playerHealth, PlayerStats.instance.playerMaxHealth);
        }
        IncreaseEnemyHealth();
    }
    public void IncreaseEnemyHealth()
    {
        foreach (var enemy in enemiesHealth)
        {
            enemy.maxHealth += enemy.maxHealth *= 0.075f;
        }
    }
    public void StartTime()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
}
