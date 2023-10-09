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
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position,target.position)<=5f)
        {
            StartCoroutine(Follow()); 
        }
    }
    public IEnumerator Follow()
    {
        coll.enabled = false;
        yield return new WaitForSeconds(minModifier-5f);
        transform.position = Vector2.Lerp(transform.position, target.transform.position, 
            Random.Range(minModifier,maxModifier) * Time.deltaTime);
        float dis = Vector2.Distance(target.position, transform.position);
        if (dis <= 1f)
        {
            coll.enabled = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ExperienceManager.instance.AddExp(50);
            Destroy(gameObject);
        }
    }
    
}
