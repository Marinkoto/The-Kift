using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Transform target;
    public float minModifier = 7f;
    public float maxModifier = 10f;
    Collider2D coll;
    private void Start()
    {
        coll = GetComponent<Collider2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        coll.enabled = false;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position,target.position)<=PlayerStats.instance.pickUpRange)
        { 
            Follow(); 
        }
        Destroy(gameObject, 13f);
    }
    public void Follow()
    {
        coll.enabled = false;
        transform.position = Vector2.Lerp(transform.position, target.transform.position, 
            Random.Range(minModifier,maxModifier) * Time.deltaTime);
        float dis = Vector2.Distance(target.position, transform.position);
        if (dis <= 1f)
        {
            coll.enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ExperienceManager.instance.AddExp(Mathf.RoundToInt(50f * PlayerStats.instance.expMultiplier));
            AudioManager.instance.PlaySFX(AudioManager.instance.lootSound);
            Destroy(gameObject);
        }
    }
    
}
