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
    public GameObject endScreenWin;
    public GameObject endScreenLose;
    public TextMeshProUGUI[] tokenCounter;
    public static bool onEnd;
    private void Start()
    {
        dungeonCounter = GetComponentInChildren<TextMeshProUGUI>();
        onEnd = false;
    }
    private void Update()
    {
        QuestManager.instance.ReachDungeon();
        SetInfo();
        if(dungeonController.dungeonNumber >= 6)
        {
            dungeonLevel++;
        }
        if (!LoadingScreeen.loadingScreenON && dungeonLevel == 4)
        {
            int tokens = Random.Range(80, 100);
            PlayfabManager.instance.AddCurrency(tokens);
            EnableWinScreen(tokens);
        }
    }
    private void SetInfo()
    {
        dungeonController = GameObject.FindGameObjectWithTag("PCG").GetComponent<DungeonController>();
        dungeonCounter.text = $"{dungeonLevel}/{dungeonController.dungeonNumber}";
    }
    public void EnableWinScreen(int tokens)
    {
        onEnd = true;
        SetTokenCounter(tokens);
        endScreenWin.SetActive(true);
        dungeonLevel++;
        Cursor.visible = true;
        DungeonManager.instance.IncreaseCount();
        DungeonManager.instance.Load();
        Invoke("StopTime" , 0.49f);
    }
    public void EnableLoseScreen(int tokens)
    {
        onEnd = true;
        SetTokenCounter(tokens);
        endScreenLose.SetActive(true);
        Cursor.visible = true;
        Invoke("StopTime", 0.49f);
    }
    public void OnDestroy()
    {
        Cursor.visible = false;
    }
    public void SetTokenCounter(int tokens)
    {
        foreach (var token in tokenCounter)
        {
            token.text = tokens.ToString();
        }
    }
    public void StopTime()
    {
        Time.timeScale = 0;
        PauseMenu.canPause = false;
    }
}
