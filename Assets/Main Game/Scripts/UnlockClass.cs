using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockClass : MonoBehaviour
{
    public static UnlockClass instance;
    public static bool fishBought;
    public static bool mageBought;
    public static bool axeBought;
    public Button fishButton;
    public Button mageButton;
    public Button axeButton;
    private void Start()
    {
        fishButton.interactable = Convert.ToBoolean(PlayerPrefs.GetInt("fishBought"));
        mageButton.interactable = Convert.ToBoolean(PlayerPrefs.GetInt("mageBought"));
        axeButton.interactable = Convert.ToBoolean(PlayerPrefs.GetInt("axeBought"));
    }
    private void OnEnable()
    {
        instance = this;
    }
}
