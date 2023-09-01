using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private WaveSpawner[] waveSpawner;
    public Transform player;
    private bool oneTime = false;
    public ClassesHandler classes;
    private void Start()
    {
        waveSpawner = GetComponentsInChildren<WaveSpawner>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = classes.classSelected;
        if (player&&!oneTime)
        {
            foreach (var wave in waveSpawner)
            {
                wave.canSpawn = true;
                StartCoroutine(wave.Spawn());
                oneTime = true;
            }
        }
        
    }
   
}
