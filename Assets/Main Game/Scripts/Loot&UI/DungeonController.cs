using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public GameObject dungeon;
    public int dungeonNumber = 1;
    public Transform player;
    public GameObject bossRoom;
    public Transform bossFightPosition;
    private bool didStart = false;
    public GameObject boss;
    public DialogueTrigger dialogueTrigger;
    public void IncreaseDungeon()
    {
        dungeonNumber++;
    }
    private void Start()
    {
        didStart = false;
        dialogueTrigger.gameObject.SetActive(true);
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
            Instantiate(dungeon, transform.position, Quaternion.identity);
            dungeonNumber = 1;
            DestroyImmediate(gameObject);
        }
    }

    public void SetPositon()
    {
        player.position = bossFightPosition.position;
        boss.SetActive(true);
        CameraShake.instance.ChangeFOV(true);
    }
    
}
