using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueShakeCamera : MonoBehaviour
{
    void Start()
    {
        CameraShake.instance.ShakeCamera(0.75f, 8f);
    }
}
