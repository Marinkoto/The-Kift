using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonInfo : MonoBehaviour
{
    TextMeshProUGUI dungeonCounter;
    DungeonController dungeonController;
    public int dungeonLevel = 1;
    private void Start()
    {
        dungeonCounter = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        SetInfo();
        
        if(dungeonController.dungeonNumber >= 6)
        {
            dungeonLevel++;
        }
        if (dungeonLevel == 4)
        {
            SceneManager.LoadScene(1);
            PlayfabManager.instance.AddCurrency(Random.Range(50, 80));
        }
    }
    private void SetInfo()
    {
        dungeonController = GameObject.FindGameObjectWithTag("PCG").GetComponent<DungeonController>();
        dungeonCounter.text = $"{dungeonLevel}/{dungeonController.dungeonNumber}";
    }
}
