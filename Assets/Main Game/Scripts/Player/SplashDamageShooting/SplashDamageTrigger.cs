using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamageTrigger : MonoBehaviour
{
    public PlayerShoot playerShootScript;
    public Collider2D playerColl;
    private void Start()
    {
        playerShootScript.splashable = false;
        
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
        if (playerColl&&Input.GetKeyDown(KeyCode.E))
        {
            playerShootScript.splashable = true;
        }
    }
}
