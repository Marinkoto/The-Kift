using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounterListener : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI bulletCounter;
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(UpdateBulletCounter);
    }
    void UpdateBulletCounter()
    {
        PlayerStats.instance.SetBulletCounter(bulletCounter);
    }
}
