using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesActivate : MonoBehaviour
{
    private Transform target;
    public GameObject[] enemies;
    private bool areSpawned = false;
    private void Start()
    {
        target = ClassesHandler.instance.classSelected;
    }
    private void Update()
    {
        target = ClassesHandler.instance.classSelected;
        if (target == null) return;
        float distance = Vector2.Distance(transform.position, target.position);
        if (target == null) return;
        if (distance < 5f && !areSpawned)
        {
            StartCoroutine(SpawnEnemies());
        }
    }
    private IEnumerator SpawnEnemies()
    {
        areSpawned = true;
        for (int i = 0; i < enemies.Length; i++)
        {
            yield return new WaitForSeconds(0.75f);
            enemies[i].SetActive(true);
            if (enemies[i] == null)
            {
                yield return null;
            }
        }
    }
}
