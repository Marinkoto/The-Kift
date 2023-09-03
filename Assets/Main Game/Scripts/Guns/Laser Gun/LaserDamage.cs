using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    private Collider2D Collider;
    private HitStop hitStopEffect;
    private void Start()
    {
        hitStopEffect = FindObjectOfType<HitStop>();
        Collider = GetComponent<Collider2D>();
        Collider.enabled = true;
    }
    private void Update()
    {
        Destroy(gameObject, 0.2f);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitColls = Physics2D.OverlapCircleAll(transform.position, 0.7f);
        foreach (var hitColl in hitColls)
        {
            var enemy = hitColl.GetComponent<EnemyHealth>();
            
            if (enemy)
            {
                hitStopEffect.HitStopEffect(0.02f);
                CameraShake.instance.ShakeCamera(0.35f, 0.4f);
                enemy.TakeDamage(PlayerStats.instance.playerDamage);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}
