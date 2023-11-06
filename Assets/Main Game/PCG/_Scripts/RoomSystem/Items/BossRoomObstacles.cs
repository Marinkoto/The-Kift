using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomObstacles : MonoBehaviour
{
    public ParticleSystem destroyEffect;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Axe"))
        {
            Destroy(gameObject);
        }
    }
    public void OnDestroy()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.itemDestroy);
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
    }
}
