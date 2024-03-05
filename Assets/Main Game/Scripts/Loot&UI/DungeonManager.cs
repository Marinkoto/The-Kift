using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public int dungeonCounter = 2;
    public static DungeonManager instance;
    private const string SaveKey = "Counter";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void Start()
    {
        Load();
        DontDestroyOnLoad(gameObject);
    }
    public void IncreaseCount()
    {
        dungeonCounter++;
        SaveDungeon();
    }
    public void Load()
    {
        if (dungeonCounter <= 0)
        {
            dungeonCounter = 2;
        }
        else
        {
            dungeonCounter = PlayerPrefs.GetInt(SaveKey);
        }
    }
    public void SaveDungeon()
    {
        PlayerPrefs.SetInt(SaveKey, dungeonCounter);
        PlayerPrefs.Save();
    }
}
