using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera Camera;
    private float shakeTimer;
    public static CameraShake instance{ get; private set; }
    public void Awake()
    {
        if (instance != null&&instance!=this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        Camera = GetComponent<CinemachineVirtualCamera>();
    }
    public void ShakeCamera(float intesity,float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin=
            Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intesity;
        shakeTimer = time;
    }
    private void Update()
    {
        UnityEngine.Camera.main.transform.rotation = Quaternion.identity;
        if (shakeTimer>0f)
        {
            shakeTimer-= Time.deltaTime;
            if (shakeTimer<=0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;    
            }
        }
        
    }
}
