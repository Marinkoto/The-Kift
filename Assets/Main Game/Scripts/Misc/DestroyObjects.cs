using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 0.5f);
    }
}
