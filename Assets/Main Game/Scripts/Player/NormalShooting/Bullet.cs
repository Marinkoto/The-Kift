using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public ParticleSystem ps;
    public float critChance = 25f;

    public TextMeshPro damagePopUp;

    public bool crit = false;
    public float critMultiplier = 1.5f;

    public GameObject chainLightningEffect;
    public GameObject beenStruck;
    public int amountToChain;
    public bool chainsLightning = false;

    public AudioClip classHitWallSound;
    private void Update()
    {
          Destroy(gameObject,6f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //lightning chain + crit check
        if (chainsLightning && collision.gameObject.tag == "Enemy")
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.shootLightning);
            AudioManager.instance.PlayHitSFX(AudioManager.instance.shootLightning);
            FindObjectOfType<HitStop>().HitStopEffect(0.031f);
            CameraShake.instance.ShakeCamera(.61f, 0.5f);
            Instantiate(ps, transform.position, Quaternion.identity);
            Instantiate(beenStruck, collision.transform);
            chainLightningEffect.GetComponent<ChainLightning>().damage = PlayerStats.instance.playerDamage;
            chainLightningEffect.GetComponent<ChainLightning>().amountToChain = amountToChain;
            Instantiate(chainLightningEffect, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }

        //destroy when hit wall
        if (collision.gameObject.tag == "Wall")
        {
            Instantiate(ps, transform.position, Quaternion.identity);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(classHitWallSound);
        }
        //enemy take damage after all
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            AudioManager.instance.PlayHitSFX(AudioManager.instance.enemyHit);
            FindObjectOfType<HitStop>().HitStopEffect(0.02f);
            CameraShake.instance.ShakeCamera(.2f, 0.5f);
            enemy.TakeDamage(PlayerStats.instance.playerDamage);
            Instantiate(ps, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag=="EnemyBullet")
        {
            Instantiate(ps, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
    
   
}
