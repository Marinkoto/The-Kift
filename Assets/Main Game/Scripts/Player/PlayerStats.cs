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
    public int bulletAmount;
    public int maxClipSize;
    public bool canDash;
    [Range(1f,5f)]
    public float armor;
    public bool hasAxe;
    public float reloadTime;
    public int currentClip;
    public int currentAmmo;
    public float dashCooldown = 1f;
    public int pickUpRange;
    public float expMultiplier;
    public float maxExp;
    public float level = 1;
    public GameObject[] statsUI;
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
        SetExpBar();
    }
    private void Update()
    {
        
        startPos = GameObject.Find("Start Pos").gameObject.transform;
        if (currentExp >= maxExp)
        {
            LevelUp();
        }  
    }
    private void OnEnable()
    {
        ExperienceManager.instance.OnExperienceChange += HandleExpChange;
    }
    private void OnDisable()
    {
        ExperienceManager.instance.OnExperienceChange -= HandleExpChange;
    }
    private void HandleExpChange(int newExp)
    {
        currentExp += newExp;
        SetExpBar();
        if (currentExp >= maxExp)
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
        SetExpBar();
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
        int randomUI = Random.Range(0, statsUI.Length);
        statsUI[randomUI].SetActive(true);
    }
    public void StatsTextSet(TextMeshProUGUI statText)
    {
        statText.text = $"Max HP: {playerMaxHealth}\nDamage: {playerDamage:f2}\n" +
            $"Movement Speed: {playerMoveSpeed:f2}\nBullets Shot: {bulletAmount}\nMax Bullets: {maxClipSize}\nDash Cooldown: {dashCooldown:f2}\n" +
            $"Reload Cooldown: {reloadTime:f2}\n Armor: {armor}";
    }
    public void SetExpBar()
    {
        levelText.text = $"LEVEL {level}";
        expBar.value = currentExp;
        expBar.maxValue = maxExp;
        expText.text = $"{currentExp}/{maxExp}";
    }
    public void SetBulletCounter(TextMeshProUGUI bulletCounter)
    {
        bulletCounter.text = $"{currentClip}/{maxClipSize}";
    }
}
