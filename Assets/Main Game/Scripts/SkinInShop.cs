using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using PlayFab.ClientModels;
using PlayFab;
using UnityEditor;

public class SkinInShop : MonoBehaviour
{
    [SerializeField] private SSkinInfo skinInfo;
    public SSkinInfo _skinInfo { get { return skinInfo; } }

    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image skinImage;

    [SerializeField] private bool isSkinUnlocked;//SFD
    [SerializeField] private bool isFreeSkin;
    public SkinManager skinManager;
    public bool clearPlayerPrefs;
    bool canBuy;
    string currentId;
    private void Awake()
    {
        if (isFreeSkin)
        {
            PlayerPrefs.SetInt(skinInfo._skinID.ToString(), 1);
            buttonText.text = "EQuip"; 
        }
        Debug.Log(PlayerPrefs.GetInt(skinInfo._skinID.ToString()));
        IsSkinUnlocked();
    }
    private void Start()
    {
        if (clearPlayerPrefs)
        {
            PlayerPrefs.DeleteKey(skinInfo._skinID.ToString());
        }
    }
    
    private void IsSkinUnlocked()
    {
        if (PlayerPrefs.GetInt(skinInfo._skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;
            buttonText.text = "EQuip";
        }
        else if(PlayerPrefs.GetInt(skinInfo._skinID.ToString()) == 0)
        {
            buttonText.text = "Buy";
        }
    }

    public void OnButtonPress()
    {
        if (isSkinUnlocked)
        {
            //equip
            skinManager.EquipSkin(this);
        }
        else if(!isSkinUnlocked)
        {
            TryRemoveMoney(skinInfo._skinPrice);
        }
    }
    public void TryRemoveMoney(int tokensPrice)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CT",
            Amount = tokensPrice,
        };
        if (PlayfabManager.instance.tokens>=tokensPrice)
        {
            PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractCoinsSuccess, OnError);
        }
        else
        {
            PlayfabManager.instance.message.text = "Not enough tokens";
        }
    }
    private void OnError(PlayFabError error)
    {
        return;
    }

    private void OnSubtractCoinsSuccess(ModifyUserVirtualCurrencyResult result)
    {
        PlayerPrefs.SetInt(skinInfo._skinID.ToString(), 1);
        IsSkinUnlocked();
        PlayfabManager.instance.GetVirtualCurrencies();
    }
}