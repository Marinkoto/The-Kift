using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameAndQuit : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void EnterGame(int indexOfScene)
    {
        SceneManager.LoadScene(indexOfScene);
        Time.timeScale = 1.0f;
    }
    
}
