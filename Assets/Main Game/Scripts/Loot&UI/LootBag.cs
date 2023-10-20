using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject itemPrefab;
    public List<HealthPotion> lootList = new List<HealthPotion>();
    HealthPotion GetDroppedItem()
    {
        int randNum = Random.Range(0, 101);
        List<HealthPotion> possibleItems = new List<HealthPotion>();
        foreach (HealthPotion item in lootList)
        {
            if (randNum<=item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count>0)
        {
            HealthPotion droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        return null;
    }
    public void SpawnLoot(Vector3 spawnPos)
    {
        HealthPotion droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(itemPrefab,spawnPos,Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;
        }
    }
}
