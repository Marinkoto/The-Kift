using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassesUI : MonoBehaviour
{
    public GameObject UI;
    void Awake()
    { 
        Invoke("ActivateUI", 1.25f);
    }
    private void ActivateUI()
    {
        UI.SetActive(true);
    }
}
