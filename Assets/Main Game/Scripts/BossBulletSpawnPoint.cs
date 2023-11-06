using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletSpawnPoint : MonoBehaviour
{
    enum SpawnType { Straight,Spin}
    [Header("Bullet Attributes")]
    [SerializeField] public GameObject bossBullet;
    [SerializeField] private SpawnType spawnType;
    [SerializeField] private float firingRate = 1.0f;
    public float speed = 1f;
    public float bulletLife;
    public bool canShoot = true;
    private GameObject spawnedBullet;
    private float timer = 0f;
    private float timerBetweenBursts;
    private void Start()
    {
        canShoot = true;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (spawnType == SpawnType.Spin)
        {
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        }
        if (timer >= firingRate && canShoot)
        {
            Fire();
            timer = 0f;
        }
        

    }
    public void Fire()
    {
        if (bossBullet)
        {
            spawnedBullet = Instantiate(bossBullet,transform.position,Quaternion.identity);
            spawnedBullet.GetComponent<BossBullets>().speed = speed;
            spawnedBullet.GetComponent<BossBullets>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
    
}
