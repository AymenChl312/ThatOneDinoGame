using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject settingsWindow;

    public Animator animator;

    public static PauseMenu instance;

    private void Awake()
    {
        animator = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();

        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PauseMenu dans la scene.");
            return;
        }
        instance = this;

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Paused()
    {
        PlayerMovement.instance.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        PlayerMovement.instance.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void MainMenuButton()
    {
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryButton()
    {
        if (CurrentSceneManager.instance.isPlayerPresentByDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedUpInThisSceneCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerHealth.instance.Respawn();
        Resume();
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }
}
