using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject crosshair;
    public GameObject healthBar;
    public GameObject expBar;
    public GameObject questMenu;
    public GameObject bulletCounter;
    public TextMeshProUGUI statText;
    public static bool isPaused;
    public static bool canPause;

    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        canPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (isPaused)
            {
                ResumeGame();
                EnableCrosshair();
                EnableHealthBar();
                EnableEXPBar();
                bulletCounter.SetActive(true);
            }
            else
            {
                PauseGame();
                DisableCrosshair();
                DisableHealthBar();
                DisableEXPBar();
                bulletCounter.SetActive(false);
                
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        PlayerStats.instance.StatsTextSet(statText);
        pauseMenu.SetActive(true);
        statText.gameObject.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        isPaused = true;
        questMenu.SetActive(false);

    }
    public void LoadScene(int indexOfScene)
    {
        SceneManager.LoadScene(indexOfScene);
        Time.timeScale = 1.0f;  
    }
    public void ResumeGame()
    {
        statText.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        isPaused = false;
        questMenu.SetActive(false);
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
    public void DisableEXPBar()
    {
        if(expBar)
            expBar.SetActive(false);
    }
    public void EnableEXPBar()
    {
        if (expBar)
            expBar.SetActive(true);
    }
}
