using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEditor;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    private int vsyncCount;
    public Toggle toggle;
    private void Start()
    {
        SetVsync(Convert.ToBoolean(PlayerPrefs.GetInt("Vsync")));
        toggle.isOn = Convert.ToBoolean(vsyncCount);
        if (!gameObject.active)
        {
            return;
        }
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentRes = 0;
        for(int i = 0; i<resolutions.Length;i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentRes;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetResolution(int resolutionIndex)
    {
        if (resolutions == null)
        {
            return;
        }
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }
    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
    public void SetVsync(bool vsync)
    {
        vsyncCount = Convert.ToInt32(vsync);
        Debug.Log(vsyncCount);
        toggle.isOn = vsync;
        QualitySettings.vSyncCount = vsyncCount;
        PlayerPrefs.SetInt("Vsync",vsyncCount);
    }
}
