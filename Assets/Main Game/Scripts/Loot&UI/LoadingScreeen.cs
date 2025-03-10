using PlayFab.EconomyModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreeen : MonoBehaviour
{
    public GameObject loadingScreen;
    public static bool loadingScreenON = false;
    public Crosshair crosshair;
    private void Start()
    {
        loadingScreen.SetActive(false);
        loadingScreenON = false;
    }
    public IEnumerator Enable()
    {
        loadingScreenON = true;
        crosshair.gameObject.SetActive(false) ;
        loadingScreen.SetActive(true);;
        PauseMenu.canPause = false;
        yield return new WaitForSeconds(5f);
        PauseMenu.canPause = true;
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
        loadingScreenON = false;
        crosshair.gameObject.SetActive(true);
    }
    public void EnableLoadingScreen()
    {
        StartCoroutine(Enable());
    }
}
