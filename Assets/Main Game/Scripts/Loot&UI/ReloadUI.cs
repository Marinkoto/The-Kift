using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadUI : MonoBehaviour
{
    public Transform objectToFollow;
    RectTransform rect;
    
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    void Start()
    {
    }

    void Update()
    {
        objectToFollow = ClassesHandler.instance.classSelected;
        if(!objectToFollow )
        {
            return;
        }
        if (objectToFollow!=null)
        {
            
            rect.anchoredPosition = objectToFollow.position;
        }
    }
}
