using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public GameObject[] dungeons;
    public int dungeonNumber = 1;
    public void IncreaseDungeon()
    {
        dungeonNumber++;
    }
    private void Update()
    {
        if (dungeonNumber == 6)
        {
            int index = Random.Range(0, dungeons.Length);
            Instantiate(dungeons[index],transform.position,Quaternion.identity);
            dungeonNumber ++;
            Destroy(gameObject);
        }
    }

}
