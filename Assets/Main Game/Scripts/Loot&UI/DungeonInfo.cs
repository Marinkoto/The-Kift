using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        if(dungeonController.dungeonNumber >= 5)
        {
            dungeonLevel++;
        }
    }
    private void SetInfo()
    {
        dungeonController = GameObject.FindGameObjectWithTag("PCG").GetComponent<DungeonController>();
        dungeonCounter.text = $"{dungeonLevel}/{dungeonController.dungeonNumber}";
    }
}
