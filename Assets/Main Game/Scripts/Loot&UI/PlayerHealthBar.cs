using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI hpCountText;
    private void Update()
    {
        if (PlayerStats.instance != null)
            hpCountText.text = Mathf.RoundToInt(PlayerStats.instance.playerHealth) + "/" + Mathf.RoundToInt(PlayerStats.instance.playerMaxHealth).ToString();
    }
    public void SetUIHealth(float health,float maxHealth)
    {
        slider.value = health;
        slider.maxValue = maxHealth;
    }
}
