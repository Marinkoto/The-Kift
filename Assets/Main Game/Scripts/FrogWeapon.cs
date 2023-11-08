using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogWeapon : MonoBehaviour
{

    public GameObject fly;
    public Vector3 attackOffset;
    public int flyCount;
    bool canSpawn;
    Boss boss;
    private void Start()
    {
        boss = GetComponent<Boss>();
        canSpawn = true;
        flyCount = 1;
    }
    public void EnragedAttack()
    {
        if (canSpawn && flyCount<=4)
        {
            if (!boss.isFlipped)
            {
                Instantiate(fly, transform.position -attackOffset, Quaternion.identity);
            }
            else
            {
                Instantiate(fly, transform.position + attackOffset, Quaternion.identity);
            }
            flyCount++;
        }
        else if(flyCount >=4)
        {
            canSpawn = false;
        }
    }

    
}
