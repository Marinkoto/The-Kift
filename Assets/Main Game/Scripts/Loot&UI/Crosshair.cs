using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Transform crosshairTransform;
    public bool isCursorVisibleOnStart = false;

    void Start()
    {
        Cursor.visible = isCursorVisibleOnStart;
    }
    private void Update()
    {
        if(!PauseMenu.isPaused)
        {
            Aiming();
        }
    }
    void Aiming()
    {
        Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
        crosshairTransform.transform.position = mousePos;
    }


}
