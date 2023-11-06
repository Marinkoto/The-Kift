using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossFly : MonoBehaviour
{
    FrogWeapon weapon;
    private void OnDestroy()
    {
        weapon.flyCount--;
    }
}
