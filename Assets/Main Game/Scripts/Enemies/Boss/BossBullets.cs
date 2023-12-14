using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullets : MonoBehaviour
{
    public float bulletLife = 1f;
    public float rotation = 0f;
    public float speed = 1f;
    private Vector2 spawnPoint;
    private float timer = 0f;
    public float damage;
    private void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }
    private void Update()
    {
        
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }
    private Vector2 Movement(float timer)
    {
        float x =timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x,y + spawnPoint.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player) && player.canGetHit)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }   
        if (collision.gameObject.TryGetComponent<Item>(out Item item))
        {
            if (!item.nonDestructible)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
