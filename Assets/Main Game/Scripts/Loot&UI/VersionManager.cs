using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class VersionManager : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        textMeshProUGUI.text = "Version: " + "1.06";
    }
}
