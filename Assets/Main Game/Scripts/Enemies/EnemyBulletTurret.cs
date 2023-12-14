using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTurret : MonoBehaviour
{
    public float bulletSpeed;
    public float damage;
    [Range(5, 9)]
    [SerializeField] private float lifeTime = 3f;
    private Rigidbody2D rb;
    GameObject target;
    public ParticleSystem ps;
    public EnemyAiTurret[] turret;
    private EnemyStats enemyStats;

    private void Start()
    {
        enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>();
        int random = Random.Range(1, 5);
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        if (random == 1)
        {
            Vector2 moveDir = transform.right * bulletSpeed;
            rb.velocity = new Vector2(moveDir.x, moveDir.y);
        }
        if (random == 2)
        {
            Vector2 moveDir = transform.up * bulletSpeed;
            rb.velocity = new Vector2(moveDir.x, moveDir.y);
        }
        if (random == 3)
        {
            Vector2 moveDir = -transform.right * bulletSpeed;
            rb.velocity = new Vector2(moveDir.x, moveDir.y);
        }
        if (random == 4)
        {
            Vector2 moveDir = -transform.up * bulletSpeed;
            rb.velocity = new Vector2(moveDir.x, moveDir.y);
        }
        Destroy(this.gameObject, lifeTime);
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
    private void OnDestroy()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
    }
}
