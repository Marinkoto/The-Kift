using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class EnemyTank : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private int damage;
    public float deathTimer = 1f;
    private Collider2D coll;
    public ParticleSystem particles;
    private EnemyHealth enemyHealth;
    private bool didCollide = false;
    private void Start()
    {
        didCollide = false;
        damage = 50;
        coll = GetComponent<Collider2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        coll.enabled = false;
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
        if (Vector2.Distance(target.position, transform.position) <= 7f)
        {
            if (didCollide)
            {
                coll.enabled = false;
            }
            else
            {
                coll.enabled = true;
            }
        }
        if (Vector2.Distance(target.position, transform.position) <= 6f && enemyHealth.canMove 
            && !LoadingScreeen.loadingScreenON && !PauseMenu.isPaused)
        {
            Invoke("Move", 1.86f);
        }
    }
    private void GetTarget()
    {
        
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;

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
            didCollide = true;
            player.TakeDamage(damage);
            coll.enabled = false;
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject,0.1f);
        }
    }
}
