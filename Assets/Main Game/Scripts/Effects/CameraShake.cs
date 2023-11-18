using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera Camera;
    private float shakeTimer;
    private Animator animator;
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
    private void Start()
    {
        ShakeCamera(0, 0);
        animator = GetComponent<Animator>();
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
        if (Time.timeScale <= 0)
        {
            ShakeCamera(0, 0);
        }
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
    public void ChangeFOV(bool fov)
    {
        animator.SetBool("HIGH", fov);
    }
}
