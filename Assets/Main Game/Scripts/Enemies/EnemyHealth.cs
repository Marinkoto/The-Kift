using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Material matWhite;
    private Material matDef;
    SpriteRenderer sr;
    public EnemyStats enemyStats;
    public GameObject lootPrefab;
    public GameObject floatingTextPrefab;
    [HideInInspector]
    public bool canMove = true;
    public ParticleSystem particles;
    private Collider2D enemyColldider;
    bool killed;
    private void Start()
    {
        killed = false;
        enemyStats = GetComponent<EnemyStats>();
        enemyStats.health = enemyStats.maxHealth;
        sr = GetComponent<SpriteRenderer>();
        matDef = sr.material;
        enemyColldider = GetComponent<Collider2D>();
    }
    public void TakeDamage(float damage)
    {
        ShowDamage(Mathf.RoundToInt(damage).ToString());
        enemyStats.health -= damage;
        AudioManager.instance.PlayHitSFX(AudioManager.instance.enemyHit);
        sr.material = matWhite;
        if (enemyStats.health <= 0 && !killed)
        {
            Die();
        }
        else
        {
            Invoke("ResetMaterial", .15f);
        }
    }
    void ResetMaterial()
    {
        sr.material = matDef;
    }
    void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = text;
        }
    }
    void Die()
    {
        enemyColldider.enabled = false;
        canMove = false;
        Destroy(gameObject);
        sr.material = matWhite;
        Instantiate(particles, transform.position, Quaternion.identity);
        Invoke("ResetMaterial", .25f);
        GetComponent<LootBag>().SpawnLoot(transform.position);
        for (int i = 0; i < enemyStats.maxHealth / 250; i++)
        {
            Instantiate(lootPrefab, transform.position + new Vector3(Random.Range(0f, 0.5f), Random.Range(0f, 2f)), Quaternion.identity);
        }
        QuestManager.instance.EnemyKilled();
        killed = true;
    }
}
