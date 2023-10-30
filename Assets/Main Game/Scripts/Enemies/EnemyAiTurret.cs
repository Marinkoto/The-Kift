using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyAiTurret : MonoBehaviour
{
    public Transform target;
    public static EnemyAIRanged instance;
    public float distanceToShoot = 5f;
    public Transform[] firingPoints;
    public float fireRate;
    private float timeToFire;
    public GameObject bullet;
    private Collider2D coll;
    public bool canMove = false;
    private EnemyHealth enemyHealth;
    public float distanceToStop = 3f;
    public float speed = 3f;
    public bool canShoot = true;
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
        if (Vector2.Distance(target.position, transform.position) <= 6f)
        {
            coll.enabled = true;
        }
        if (Vector2.Distance(target.position, transform.position) <= 7f && enemyHealth.canMove && canMove)
        {
            Invoke("Move", 2f);
        }
        Stop();
        if (!target)
        {
            return;
        }
        if (Vector2.Distance(target.position, transform.position) <= distanceToShoot && canShoot 
            && !PauseMenu.isPaused && !LoadingScreeen.loadingScreenON)
        {
            Invoke("Shoot",1f);
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
        if (timeToFire <= 0f)
        {

            for (int i = 0; i < firingPoints.Length; i++)
            {
                Instantiate(bullet, firingPoints[i].transform.position, Quaternion.identity);
            }


            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }
    private void Move()
    {
        if (target != null)
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
