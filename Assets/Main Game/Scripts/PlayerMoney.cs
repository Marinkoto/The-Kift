using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;
using System;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance;
    bool canBuy;
    public static bool orange;

    private void Awake()
    {
        Instance = this;

        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
    }

    private void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
    }

    public bool TryRemoveMoney(int tokensPrice)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CT",
            Amount = tokensPrice,
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractCoinsSuccess, OnError);
        PlayfabManager.instance.GetVirtualCurrencies();
        return canBuy;
    }

    private void OnError(PlayFabError error)
    {
        canBuy = false;
    }

    private void OnSubtractCoinsSuccess(ModifyUserVirtualCurrencyResult result)
    {
        canBuy = true;
    }
}