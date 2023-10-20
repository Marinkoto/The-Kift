using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health,maxHealth = 50f;
    public float boundHealth;
    public Material matWhite;
    private Material matDef;
    SpriteRenderer sr;
    public GameObject lootPrefab;
    public GameObject floatingTextPrefab; 
    [HideInInspector]
    public bool canMove = true;
    public ParticleSystem particles;
    private Collider2D enemyColldider;
    private void Start()
    {
        health = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        matDef = sr.material;
        enemyColldider = GetComponent<Collider2D>();
    }
    public void TakeDamage(float damage)
    {
        ShowDamage(Mathf.RoundToInt(damage).ToString());
        health -= damage;
        AudioManager.instance.PlayHitSFX(AudioManager.instance.enemyHit);
        sr.material = matWhite;
        if (health<=0)
        {
            enemyColldider.enabled = false;
            canMove = false;
            Destroy(gameObject);
            sr.material = matWhite;
            Instantiate(particles,transform.position,Quaternion.identity);
            Invoke("ResetMaterial",.25f);
            GetComponent<LootBag>().SpawnLoot(transform.position);
            for (int i = 0; i < maxHealth/250; i++)
            {
                Instantiate(lootPrefab, transform.position + new Vector3(Random.Range(0f,0.5f),Random.Range(0f,2f)), Quaternion.identity);
            }
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
}
