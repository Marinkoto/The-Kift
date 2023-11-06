using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public GameObject[] dungeons;
    public int dungeonNumber = 1;
    public Transform player;
    public GameObject bossRoom;
    public Transform bossFightPosition;
    private bool didStart = false;
    public GameObject boss;
    public void IncreaseDungeon()
    {
        dungeonNumber++;
    }
    private void Start()
    {
        didStart = false;
    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (dungeonNumber == 5)
        {
            bossRoom.SetActive(true);
            if (!didStart)
            {
                Invoke("SetPositon", 4f);
                didStart = true;
            }
        }
        if (dungeonNumber == 6)
        {
            int index = Random.Range(0, dungeons.Length);
            Instantiate(dungeons[index],transform.position,Quaternion.identity);
            dungeonNumber = 1;
            Destroy(gameObject);
        }
        
    }

    public void SetPositon()
    {
        player.position = bossFightPosition.position;
        boss.SetActive(true);
        CameraShake.instance.ChangeFOV(true);
    }

}
