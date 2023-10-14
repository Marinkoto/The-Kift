using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreeen : MonoBehaviour
{
    public GameObject loadingScreen;
    private void Start()
    {
        loadingScreen.SetActive(false);
    }
    public IEnumerator Enable()
    {
        loadingScreen.SetActive(true);
        PauseMenu.canPause = false;
        yield return new WaitForSeconds(5f);
        PauseMenu.canPause = true;
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
    }
    public void EnableLoadingScreen()
    {
        StartCoroutine(Enable());
    }
}
