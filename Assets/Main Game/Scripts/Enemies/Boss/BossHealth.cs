using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public float health = 500;
    public float maxHealth = 500;
    //public GameObject deathEffect;
    SpriteRenderer sprite;
    Material defaultMaterial;
    public Material whiteMaterial;
    public bool isInvulnerable = false;
    public GameObject portal;
    public GameObject floatingTextPrefab;
    public Transform room;
    public BossHealthBar healthBar;
    public string bossName;
    public GameObject dialogueTrigger;
    bool killed;
    private void Awake()
    {
        healthBar.SetHealthBar();
    }
    private void Start()
    {
        killed = false;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        health = maxHealth;
        defaultMaterial = sprite.material;
        healthBar.SetBossName(bossName);
        healthBar.dungeonInfoUI.SetActive(false);
    }
    public void TakeDamage(float damage)
    {
        if (isInvulnerable)
            return;
        healthBar.SetHealthBar();
        ShowDamage(Mathf.RoundToInt(damage).ToString());
        sprite.material = whiteMaterial;

        health -= damage;
        Invoke("ResetMaterial", .25f);
        if (health < maxHealth / 2)
        {
            GetComponent<Animator>().SetBool("isEnraged", true);
        }

        if (health <= 0 && !killed)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject spawnedPortal = Instantiate(portal, transform.position, Quaternion.identity);
        spawnedPortal.transform.parent = room.transform;
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        PlayerStats.instance.playerHealth = PlayerStats.instance.playerMaxHealth;
        CameraShake.instance.ChangeFOV(false);
        healthBar.DisableBar();
        healthBar.dungeonInfoUI.SetActive(true);
        QuestManager.instance.BossKilled();
        dialogueTrigger.SetActive(true);
        killed = true;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<BossRoomObstacles>(out BossRoomObstacles obstacle))
        {
            Destroy(obstacle);
        }
    }
    void ResetMaterial()
    {
        sprite.material = defaultMaterial;
    }
    void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = text;
        }
    }
}
