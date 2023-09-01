using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamageBullet : MonoBehaviour
{
    public float splashRange = 1f;
    public Vector3 launchOffset;
    public float speed;
    [HideInInspector]
    public float lifeTime;
    private void Awake()
    {
        lifeTime = Random.Range(3, 6);
    }
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce((transform.right + 
            new Vector3(Random.Range(0, 1), Random.Range(0, 1))) * speed, ForceMode2D.Impulse);
        transform.Translate(launchOffset);
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (splashRange > 0)
        {
            var hitColls = Physics2D.OverlapCircleAll(transform.position, splashRange);
            foreach (var hitcoll in hitColls)
            {
                var enemy = hitcoll.GetComponent<EnemyHealth>();
                if (enemy)
                {
                    var closestPoint = hitcoll.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closestPoint, transform.position);
                    var damagePercent = Mathf.InverseLerp(splashRange, 0, distance);
                    enemy.TakeDamage(Mathf.RoundToInt(damagePercent * PlayerStats.instance.playerDamage));
                }
                float damageTimer = 4f;
                Destroy(gameObject, damageTimer);
            }
        }



    }
}
            
        
    
    

