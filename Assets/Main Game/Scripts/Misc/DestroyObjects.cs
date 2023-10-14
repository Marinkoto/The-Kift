using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    public float seconds;
    private void Update()
    {
        Destroy(gameObject, seconds);
    }
}
