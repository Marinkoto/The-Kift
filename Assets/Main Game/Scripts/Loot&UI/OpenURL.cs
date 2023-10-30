using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void URL(string url)
    {
        Application.OpenURL(url);
    }
}
