using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
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
        if (Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            coll.enabled = true;
            Shoot();
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
    
}
