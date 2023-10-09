using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour
{
    public Button button;
    public CorridorFirstDungeonGenerator dungeonGenerator;
    public DungeonController controller;
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(GenerateDungeon);
        button.onClick.AddListener(IncreaseDungeonNumber);
    }
    void GenerateDungeon()
    {
        dungeonGenerator.GenerateDungeon();
    }
    void IncreaseDungeonNumber()
    {
        controller.IncreaseDungeon();
    }
    private void Update()
    {
        dungeonGenerator = GameObject.Find("CorridorFirstDungeonGenerator").GetComponent<CorridorFirstDungeonGenerator>();
    }
}
