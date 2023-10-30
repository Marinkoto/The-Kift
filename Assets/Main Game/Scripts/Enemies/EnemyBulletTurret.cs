using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTurret : MonoBehaviour
{
    [Range(5, 15)]
    [SerializeField] private float speed = 2f;
    [Range(5, 9)]
    [SerializeField] private float lifeTime = 3f;
    private Rigidbody2D rb;
    GameObject target;
    public float damage;
    public ParticleSystem ps;
    public EnemyAiTurret[] turret;
    private void Start()
    {
        int random = Random.Range(1, 5);
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        if (random == 1)
        {
            Vector2 moveDir = transform.right * speed;
            rb.velocity = new Vector2(moveDir.x, moveDir.y);
        }
        if (random == 2)
        {
            Vector2 moveDir = transform.up * speed;
            rb.velocity = new Vector2(moveDir.x, moveDir.y);
        }
        if (random == 3)
        {
            Vector2 moveDir = -transform.right * speed;
            rb.velocity = new Vector2(moveDir.x, moveDir.y);
        }
        if (random == 4)
        {
            Vector2 moveDir = -transform.up * speed;
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
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bullet")
        {
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
