 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    private CircleCollider2D coll;
    public LayerMask enemyLayer;
    public float damage;
    public GameObject chainLightningEffect;
    public GameObject beenStruck;
    public int amountToChain;
    private GameObject startObject;
    private GameObject endObject;
    private Animator anim;
    public ParticleSystem particles;
    private int singleSpawn;
    void Start()
    {
        damage = PlayerStats.instance.playerDamage;
        if (amountToChain==0)
        {
            Destroy(gameObject);
        }
        startObject = gameObject;
        singleSpawn = 0;
        coll = GetComponent<CircleCollider2D>();
        anim  = GetComponent<Animator>();
        particles = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (singleSpawn == 0)
        {
            if (enemyLayer == (enemyLayer | 1 << collision.gameObject.layer) && !collision.GetComponentInChildren<EnemyStruck>())
            {
                singleSpawn = 1;
                endObject = collision.gameObject;
                amountToChain -= 1;
                Instantiate(chainLightningEffect, collision.gameObject.transform.position, Quaternion.identity);
                Instantiate(beenStruck, collision.gameObject.transform);
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                anim.StopPlayback();
                coll.enabled = false;
                Destroy(gameObject, 0.4f);
                particles.Play();
                var emitParams= new ParticleSystem.EmitParams();
                emitParams.position = startObject.transform.position;
                particles.Emit(emitParams, 1);
                emitParams.position = endObject.transform.position;
                particles.Emit(emitParams, 1);
                singleSpawn = 0;
            }
            if (singleSpawn == 0)
            {
                Destroy(gameObject,1f);
            }
        }
        else
        {
            Destroy(gameObject);
        }
       
    }
}
