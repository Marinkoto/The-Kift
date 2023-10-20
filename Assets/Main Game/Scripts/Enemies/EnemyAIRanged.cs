using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Security.Cryptography;
using TMPro;

public class EnemyAIRanged : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public static EnemyAIRanged instance; 
    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;
    public Transform firingPoint;
    public float fireRate;
    private float timeToFire;
    public GameObject bullet;
    public Transform rotatePoint;
    private EnemyHealth enemyHealth;
    private Collider2D coll;
    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        coll = GetComponent<Collider2D>();
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
        if (Vector2.Distance(target.position, transform.position) <= 7f && enemyHealth.canMove )
        {
            coll.enabled = true;
            Invoke("Move", 1.25f);
        }
        Stop();
        if (!target)
        {
            return;
        }
        if (Vector2.Distance(target.position, transform.position) <= distanceToShoot 
            && !PauseMenu.isPaused && !LoadingScreeen.loadingScreenON)
        {
            Invoke("Shoot", 0.4f);
        }
    }
    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    private void Shoot()
    {
        
        float angle = Mathf.Atan2(target.transform.position.y, target.transform.position.x) * Mathf.Rad2Deg;
        rotatePoint.rotation = Quaternion.Euler(0, 0, angle);

        if (timeToFire<=0f)
        {
            
            Instantiate(bullet, firingPoint.position,firingPoint.rotation);
           

            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }
    private void Move()
    {
        if (target!=null)
        {
            if (Vector2.Distance(target.position, transform.position) >= distanceToStop)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
            }
        }
           
    }
    void Stop()
    {
        if (!target)
        {
            return;
        }
        if ((Vector2.Distance(target.position, transform.position) >= 6f) && !target)
        {
            transform.position = new Vector2(transform.position.y, transform.position.x);
        }
        else
        {
            return;
        }
    }
}
    

