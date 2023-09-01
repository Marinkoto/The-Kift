using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMultiShot : MonoBehaviour
{
    [Range(5, 15)]
    [SerializeField] private float speed = 2f;
    [Range(5, 9)]
    [SerializeField] private float lifeTime = 3f;
    private Rigidbody2D rb;
    GameObject target;
    public float damage;
    public ParticleSystem ps;
    private void Start()
    {
        
        rb=GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject,lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            Instantiate(ps, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            

            player.TakeDamage(damage);
            Instantiate(ps, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(ps, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
