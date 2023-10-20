using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public Slider expBar;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI levelText;
    public void SetStartPosition()
    {
        Invoke("StartPosition", 1f);
    }
    public void StartPosition()
    {
        transform.position = startPos.position;
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
    private void Start()
    {
        levelText.text = $"LEVEL {level}";
        expText.text = $"{currentExp}/{maxExp}";
    }
    private void Update()
    {
        startPos = GameObject.Find("Start Pos").gameObject.transform;
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
        expBar.value = currentExp;
        expBar.maxValue = maxExp;
        expText.text = $"{currentExp}/{maxExp}";
        if (currentExp>=maxExp)
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        playerDamage += 5f;
        playerMaxHealth += 5f;
        level++;
        maxExp += 200f;
        currentExp = 0f;
        levelText.text = $"LEVEL {level}";
        expBar.value = currentExp;
        expBar.maxValue = maxExp;
        expText.text = $"{currentExp}/{maxExp}";
        if (playerHealth<=playerMaxHealth-10f)
        {
            playerHealth = playerHealth + 10;
        }
        else
        {
            playerHealth = playerMaxHealth;
        }
        AudioManager.instance.PlayLevelUPSFX(AudioManager.instance.levelUp);
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
