using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAnimation : MonoBehaviour
{
    public HandleAiming aimingController;
    public IEnumerator Reload(float duration)
    {
        aimingController.canAim = false;
        Quaternion startRot = transform.rotation;
        float timer = 0.0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.rotation = startRot * Quaternion.AngleAxis(timer / duration * 360f, Vector3.back);
            yield return null; 
        }
        transform.rotation = startRot;
        aimingController.canAim = true;
    }
}
