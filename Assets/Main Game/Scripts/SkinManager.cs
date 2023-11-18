using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{

    public static bool equippedSkin { get; private set; }
    public static bool isOrange;
    public static bool isBlack;
    public static SkinManager instance;
    public static string equippedSkins { get; private set; }
    [SerializeField] private SSkinInfo[] allSkins;
    private const string skinPref = "skinPref";

    [SerializeField] private Transform skinsInShopPanelsParent;
    [SerializeField] private List<SkinInShop> skinsInShopPanels = new List<SkinInShop>();//SFD

    [SerializeField] private Button currentlyEquippedSkinButton;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        foreach (Transform s in skinsInShopPanelsParent)
        {
            if (s.TryGetComponent(out SkinInShop skinInShop))
                skinsInShopPanels.Add(skinInShop);
        }

        EquipPreviousSkin();

        string lastSkinUsed = PlayerPrefs.GetString(skinPref);
        SkinInShop skinEquippedPanel = Array.Find(skinsInShopPanels.ToArray(), dummyFind => dummyFind._skinInfo._skinID.ToString() == lastSkinUsed);
        currentlyEquippedSkinButton = skinEquippedPanel.GetComponentInChildren<Button>();
        currentlyEquippedSkinButton.interactable = false;
    }
   
    private void EquipPreviousSkin()
    {
        string lastSkinUsed = PlayerPrefs.GetString(skinPref);
        SkinInShop skinEquippedPanel = Array.Find(skinsInShopPanels.ToArray(), dummyFind => dummyFind._skinInfo._skinID.ToString() == lastSkinUsed);
        EquipSkin(skinEquippedPanel);
    }

    public void EquipSkin(SkinInShop skinInfoInShop)
    {
        if (skinInfoInShop._skinInfo.skinID.ToString() == "Orange")
        {
            isOrange = true;
            isBlack = false;
        }
        else if(skinInfoInShop._skinInfo.skinID.ToString() == "Default")
        {
            isBlack = true;
            isOrange = false;
        }

        PlayerPrefs.SetString(skinPref, skinInfoInShop._skinInfo._skinID.ToString());
        if (currentlyEquippedSkinButton != null)
            currentlyEquippedSkinButton.interactable = true;

        currentlyEquippedSkinButton = skinInfoInShop.GetComponentInChildren<Button>();
        currentlyEquippedSkinButton.interactable = false;
    }
}