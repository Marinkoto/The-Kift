using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class EnemyTank : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public int damage;
    private Collider2D coll;
    public ParticleSystem particles;
    private EnemyHealth enemyHealth;
    public bool selfDestroyed;
    bool canDamage = true;
    private void Start()
    {
        coll = GetComponent<Collider2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        coll.enabled = false;
        canDamage = true;
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
            coll.enabled = true;
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
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player) && selfDestroyed)
        {
            player.TakeDamage(damage);
            coll.enabled = false;
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(!selfDestroyed)
        {
            if (canDamage)
            {
                player.TakeDamage(damage);
                StartCoroutine(DamageBool());
            }
            Instantiate(particles, transform.position, Quaternion.identity);
        }
    }
    IEnumerator DamageBool()
    {
        coll.enabled = false;
        canDamage = false;
        yield return new WaitForSeconds(0.75f);
        canDamage = true;
        coll.enabled = true;
    }

}
