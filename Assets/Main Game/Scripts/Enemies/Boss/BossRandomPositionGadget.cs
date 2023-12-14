using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRandomPositionGadget : MonoBehaviour
{
    Vector2 randomPosition;
    public float xRange;
    public float yRange;
    void Start()
    {
        float xPosition = Random.Range(10 + xRange, 306);
        float yPosition = Random.Range(0 - yRange, 0 + yRange);
        randomPosition = new Vector2(xPosition, yPosition);
        transform.position = randomPosition;
    } 
}
