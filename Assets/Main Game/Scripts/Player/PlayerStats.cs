using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance { get; private set; }

    public float playerDamage;
    public float playerHealth;
    public float playerMaxHealth;
    public float playerMoveSpeed;
    public float currentExp;
    public float maxExp;
    public float level = 1;
    public GameObject statsUI;
    public Transform startPos;
    public void SetStartPosition()
    {
        transform.position = Vector2.zero;
        transform.position = new Vector2(startPos.position.x,startPos.position.y);
    }
    private void Awake()
    {
        instance = this;
        if (instance != this || instance == null)
        {
            Destroy(this);
        }
        else
            instance = this;
    }
    private void OnEnable()
    {
        ExperienceManager.instance.OnExperienceChange += HandleExpChange;
        currentExp += currentExp;
    }
    private void OnDisable()
    {
        ExperienceManager.instance.OnExperienceChange -= HandleExpChange;
    }
    private void HandleExpChange(int newExp)
    {
        currentExp += newExp;
        if (currentExp>=maxExp)
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        playerDamage += 5f;
        playerHealth += 10f;
        level++;
        maxExp += 100f;
        currentExp = 0f;
        if (playerHealth<=playerMaxHealth-50f)
        {
            playerHealth = playerHealth += 50;
        }
        else
        {
            playerHealth = playerMaxHealth;
        }
        AudioManager.instance.PlaySFX(AudioManager.instance.levelUp);
    }
    public void ActivateUI()
    {
        if (!statsUI)
        {
            return;
        }
        statsUI.SetActive(true);
        
    }
}
