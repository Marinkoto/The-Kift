using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class BuyClass : MonoBehaviour
{
    public enum ClassType { Mage, Fish, Axe, Smg }
    public ClassType classType;
    public int tokensPrice; 
    public PlayfabManager manager;
    void Start()
    {
        UnlockClass.axeBought = Convert.ToBoolean(PlayerPrefs.GetInt("axeBought"));
        UnlockClass.fishBought = Convert.ToBoolean(PlayerPrefs.GetInt("fishBought"));
        UnlockClass.mageBought = Convert.ToBoolean(PlayerPrefs.GetInt("mageBought"));
        UnlockClass.smgBought = Convert.ToBoolean(PlayerPrefs.GetInt("smgBought"));
    }
    public void BuyClasses()
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CT",
            Amount = tokensPrice,
        };
        if (PlayfabManager.instance.tokens >= tokensPrice)
        {
            PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractCoinsSuccess, OnError);
        }
        else
        {
            PlayfabManager.instance.message.text = "Not enough tokens";
        }
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
        if (classType == ClassType.Smg)
        {
            UnlockClass.smgBought = true;
            PlayerPrefs.SetInt("smgBought", 1);
            manager.smgButton.interactable = false;
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
