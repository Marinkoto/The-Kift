using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossFly : MonoBehaviour
{
    public FrogWeapon weapon;
    private void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Boss").GetComponent<FrogWeapon>();
    }
    private void OnDestroy()
    {
        weapon.flyCount--;
    }
}
