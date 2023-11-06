using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    public TextMeshPro damagePopUp;

    public GameObject chainLightningEffect;
    public GameObject beenStruck;
    public int amountToChain;
    public bool chainsLightning = false;
    public bool canBeDestroyedBySelf;
    public AudioClip classHitWallSound;
    private void Update()
    {
          Destroy(gameObject,6f);
    }
    //for enemies which have colliders that are triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && canBeDestroyedBySelf)
        {
            Destroy(gameObject);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            AudioManager.instance.PlaySFX(classHitWallSound);
        }
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            AudioManager.instance.PlayHitSFX(AudioManager.instance.enemyHit);
            FindObjectOfType<HitStop>().HitStopEffect(0.02f);
            ShakeCamera();
            enemy.TakeDamage(PlayerStats.instance.playerDamage);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (chainsLightning && collision.gameObject.tag == "Enemy")
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.shootLightning);
            AudioManager.instance.PlayHitSFX(AudioManager.instance.shootLightning);
            FindObjectOfType<HitStop>().HitStopEffect(0.031f);
            ShakeCamera();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Instantiate(beenStruck, collision.transform);
            chainLightningEffect.GetComponent<ChainLightning>().amountToChain = amountToChain;
            Instantiate(chainLightningEffect, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
    //for enemies which have normal colliders
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<BossHealth>(out BossHealth boss))
        {
            boss.TakeDamage(PlayerStats.instance.playerDamage);
            Destroy(gameObject);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            AudioManager.instance.PlaySFX(classHitWallSound);
        }
        if (collision.gameObject.CompareTag("Bullet") && canBeDestroyedBySelf)
        {
            Destroy(gameObject);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            AudioManager.instance.PlaySFX(classHitWallSound);
        }
        //lightning chain 
        if (chainsLightning && collision.gameObject.tag == "Enemy")
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.shootLightning);
            AudioManager.instance.PlayHitSFX(AudioManager.instance.shootLightning);
            FindObjectOfType<HitStop>().HitStopEffect(0.031f);
            ShakeCamera();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Instantiate(beenStruck, collision.transform);
            chainLightningEffect.GetComponent<ChainLightning>().amountToChain = amountToChain;
            Instantiate(chainLightningEffect, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }

        //destroy when hit wall
        if (collision.gameObject.tag == "Wall")
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(classHitWallSound);
        }
        if (collision.gameObject.TryGetComponent<Item>(out Item item) && !item.nonDestructible)
        {
            Destroy(collision.gameObject);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Items")
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(classHitWallSound);
        }
        //enemy take damage after all
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            AudioManager.instance.PlayHitSFX(AudioManager.instance.enemyHit);
            FindObjectOfType<HitStop>().HitStopEffect(0.02f);
            ShakeCamera();
            enemy.TakeDamage(PlayerStats.instance.playerDamage);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag=="EnemyBullet")
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } 
    }
    public void ShakeCamera()
    {
        if (!PauseMenu.isPaused && PauseMenu.canPause)
        {
            CameraShake.instance.ShakeCamera(.21f, 0.5f);
        }
    }
   
}
