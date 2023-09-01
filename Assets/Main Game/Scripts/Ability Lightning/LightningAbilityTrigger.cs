using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAbilityTrigger : MonoBehaviour
{
    public Bullet bulletScript;
    public Collider2D playerColl;
    private void Start()
    {      
        bulletScript.chainsLightning = false;
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnTriggerEnter2D(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerColl && Input.GetKeyDown(KeyCode.E))
        {
            bulletScript.chainsLightning = true;
        }
    }
    
}

