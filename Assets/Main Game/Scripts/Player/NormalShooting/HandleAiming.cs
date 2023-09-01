using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Threading;

public class HandleAiming : MonoBehaviour
{
    private Transform aimTransform;
    private void Start()
    {
        aimTransform = transform.Find("Aim");
    }
    private void Aiming()
    {
        Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDir = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        Vector3 localScale = Vector3.one;
        if (angle>90 || angle<-90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = +1f;
        }
        aimTransform.localScale = localScale;
    }
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            Aiming();
        }
    }
}
