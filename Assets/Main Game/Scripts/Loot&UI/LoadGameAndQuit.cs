using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class LoadGameAndQuit : MonoBehaviour
{
    public TextMeshProUGUI message;
    public void QuitGame()
    {
        Application.Quit();
    }
    public void EnterGame(int indexOfScene)
    {
        if (!SkinManager.isBlack && !SkinManager.isOrange)
        {
            message.text = "You need to select a skin in the shop";
            return;
        }
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            SceneManager.LoadScene(indexOfScene);
            Time.timeScale = 1.0f;
        }
        else
        {
            message.text = "You need to login first";
            return;
        }
    }
    
}
