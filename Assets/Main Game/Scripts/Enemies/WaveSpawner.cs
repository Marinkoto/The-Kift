using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefabs;
    public Transform player;
     public bool canSpawn;
    int enemyCount = 0;
    public int enemies = 2;
    private void Awake()
    {
        canSpawn = false;
    }
    private void Update()
    {
        if (enemyCount == enemies)
        {
            canSpawn = false;
        }
    }
    public IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawn && !PauseMenu.isPaused)
        {
            yield return wait;
            int index = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[index];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            enemyCount++;
        }
    }
    

}


