using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class BuyClass : MonoBehaviour
{
    public enum ClassType { Mage, Fish, Axe }
    public ClassType classType;
    public int tokensPrice; 
    public PlayfabManager manager;
    void Start()
    {
        UnlockClass.axeBought = Convert.ToBoolean(PlayerPrefs.GetInt("axeBought"));
        UnlockClass.fishBought = Convert.ToBoolean(PlayerPrefs.GetInt("fishBought"));
        UnlockClass.mageBought = Convert.ToBoolean(PlayerPrefs.GetInt("mageBought"));
    }
    public void BuyClasses()
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CT",
            Amount = tokensPrice,
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractCoinsSuccess, OnError);
    }

    private void OnSubtractCoinsSuccess(ModifyUserVirtualCurrencyResult result)
    {
        if (classType == ClassType.Mage)
        {
            UnlockClass.mageBought = true;
            PlayerPrefs.SetInt("mageBought", 1);
            manager.mageButton.interactable = false;
        }
        if (classType == ClassType.Fish)
        {
            UnlockClass.fishBought = true;
            PlayerPrefs.SetInt("fishBought", 1);
            manager.fishButton.interactable = false;
        }
        if (classType == ClassType.Axe)
        {
            UnlockClass.axeBought = true;
            PlayerPrefs.SetInt("axeBought", 1);
            manager.axeButton.interactable = false;
        }
        manager.GetVirtualCurrencies();
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
