using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogWeapon : MonoBehaviour
{

    public GameObject gadget;
    public Vector3 attackOffset;
    public int gadgetCount;
    bool canSpawn;
    Boss boss;
    private void Start()
    {
        boss = GetComponent<Boss>();
        canSpawn = true;
        gadgetCount = 1;
    }
    public void EnragedAttack()
    {
        if (canSpawn && gadgetCount <= 4)
        {
            if (!boss.isFlipped)
            {
                Instantiate(gadget, transform.position -attackOffset, Quaternion.identity);
            }
            else if(boss.isFlipped) 
            {
                Instantiate(gadget, transform.position + attackOffset, Quaternion.identity);
            }
            else
            {
                Instantiate(gadget, transform.position, Quaternion.identity);
            }
            gadgetCount++;
        }
        else if(gadgetCount >= 4)
        {
            canSpawn = false;
        }
    }

    
}
