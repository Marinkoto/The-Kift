using CodeMonkey.MonoBehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassesHolder : MonoBehaviour
{
    public static ClassesHolder classes { get; private set; }
    public GameObject techClass;
    public GameObject fishClass;
    public GameObject mageClass;
    private void Awake()
    {
        classes = this;

        classes = this;
        if (classes != null && classes != this)
        {
            Destroy(this);
        }
        else
        {
            classes = this;
        }
    }
}
