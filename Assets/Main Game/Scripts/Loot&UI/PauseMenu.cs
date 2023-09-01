using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject crosshair;
    public GameObject healthBar;

    public static bool isPaused;
    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
                EnableCrosshair();
                EnableHealthBar();
            }
            else
            {
                PauseGame();
                DisableCrosshair();
                DisableHealthBar();
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        isPaused = true;
    }
    public void MainMenuEntry(int indexOfScene)
    {
        SceneManager.LoadScene(indexOfScene);
        Time.timeScale = 1.0f;  
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        isPaused = false;
    }
    public void DisableCrosshair()
    {
        crosshair.SetActive(false);
    }
    public void EnableCrosshair()
    {
        crosshair.SetActive(true);
    }
    public void DisableHealthBar()
    {
        healthBar.SetActive(false);
    }
    public void EnableHealthBar()
    {
        healthBar.SetActive(true);
    }
}
