using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreeen : MonoBehaviour
{
    public GameObject loadingScreen;
    public static bool loadingScreenON = false;
    public GameObject audioManager;
    private void Start()
    {
        audioManager = GameObject.Find("Audio Manager");
        loadingScreen.SetActive(false);
        loadingScreenON = false;
    }
    public IEnumerator Enable()
    {
        loadingScreenON = true;
        loadingScreen.SetActive(true);
        audioManager.SetActive(false);
        PauseMenu.canPause = false;
        yield return new WaitForSeconds(5f);
        PauseMenu.canPause = true;
        audioManager.SetActive(true);
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
        loadingScreenON = false;
    }
    public void EnableLoadingScreen()
    {
        StartCoroutine(Enable());
    }
}
