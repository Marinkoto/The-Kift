using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private Rigidbody2D rb;
    private int damage;
    public float deathTimer = 1f;
    private Collider2D Collider;
    public ParticleSystem particles;
    private EnemyHealth enemyHealth;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = 50;
        Collider = GetComponent<Collider2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        Collider.enabled = false;
    }

    private void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        if (!target)
        {
            return;
        }
        if (Vector2.Distance(target.position, transform.position) <= 5.5f && enemyHealth.canMove)
        {
            
            Move();
        }

       
    }
    private void GetTarget()
    {
        
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            Collider.enabled = true;

        }
    }
   
    private void Move()
    {
        if (target != null)
        {
             transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime); 
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            player.TakeDamage(damage);
            Collider.enabled = false;
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject,0.1f);
        }
    }
}
