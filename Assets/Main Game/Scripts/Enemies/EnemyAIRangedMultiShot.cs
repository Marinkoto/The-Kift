using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Security.Cryptography;
using TMPro;
using System;

public class EnemyAIRangedMultiShot : MonoBehaviour
{
    public Transform target;
    public static EnemyAIRanged instance;
    public EnemyStats enemyStats;
    public Transform[] firingPoints;
    private float timeToFire;
    public GameObject bullet;
    public Transform rotatePoint;
    private EnemyHealth enemyHealth;
    private Collider2D coll;
    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
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
        else if (!target)
        {
            return;
        }
        else if (Vector2.Distance(target.position, transform.position) <= 7f)
        {
            coll.enabled = true;
        }
        else if (Vector2.Distance(target.position, transform.position) <= 6f && enemyHealth.canMove )
        {
            Invoke("Move", 3f);
        }
        Stop();
        if (!target)
        {
            return;
        }
        else if (Vector2.Distance(target.position, transform.position) <= enemyStats.distanceToShoot && 
            !PauseMenu.isPaused && !LoadingScreeen.loadingScreenON)
        {
            Invoke("Shoot", 1f);
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

            for (int i = 0; i < firingPoints.Length; i++)
            {
                Instantiate(bullet, firingPoints[i].transform.position, Quaternion.identity);
            }
           

            timeToFire = enemyStats.fireRate;
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
            if (Vector2.Distance(target.position, transform.position) >= enemyStats.distanceToStop)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, enemyStats.moveSpeed * Time.deltaTime);
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
    

