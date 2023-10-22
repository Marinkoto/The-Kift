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
        dungeonController = GameObject.FindGameObjectWithTag("PCG").GetComponent<DungeonController>();
    }
    private void Update()
    {
        SetInfo();
        if(dungeonController.dungeonNumber == 5)
        {
            SetInfo();
            dungeonLevel++;
        }
    }
    private void SetInfo()
    {
        dungeonCounter.text = $"{dungeonLevel}/{dungeonController.dungeonNumber}";
    }
}
