using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadUI : MonoBehaviour
{
    public Transform objectToFollow;
    RectTransform rect;
    [SerializeField] Vector3 offset;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    void Update()
    {
        objectToFollow = GameObject.FindGameObjectWithTag("Player").transform;
        if (!objectToFollow )
        {
            return;
        }
        if (objectToFollow!=null)
        {
            rect.anchoredPosition = objectToFollow.position+offset;
        }
    }
}
