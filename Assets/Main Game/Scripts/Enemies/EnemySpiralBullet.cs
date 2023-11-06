using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpiralBullet : MonoBehaviour
{
    private Vector2 moveDirection;
    public float moveSpeed;
    private float angle = 0f;
    public ParticleSystem ps;
    public float damage = 0f;
    void Start()
    {
        InvokeRepeating("Fire", 0f,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 13f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }
    private void Fire()
    {
        float bulletDirectionX = transform.position.x + Mathf.Sin(angle * Mathf.PI / 178f);
        float bulletDirectionY = transform.position.y + Mathf.Cos(angle * Mathf.PI / 180f);
        Vector3 bulletMoveDirection = new Vector3(bulletDirectionX,bulletDirectionY, 0f);
        Vector2 bulletDirection = (bulletMoveDirection-transform.position).normalized;
        transform.position = transform.position;
        transform.rotation = transform.rotation;
        gameObject.SetActive(true);
        SetMoveDirection(bulletDirection);
        angle += 10f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        
    }
    private void OnDestroy()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
    }
}
