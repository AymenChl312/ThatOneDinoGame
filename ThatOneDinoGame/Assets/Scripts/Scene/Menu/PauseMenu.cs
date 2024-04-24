using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject settingsWindow;

    private Animator fadeSystem;
    public Animator bulles;
    public Animator pauseText;

    public static PauseMenu instance;

    private void Awake()
    {

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
        StartCoroutine(PauseOpen());
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        if(DialogueManager.instance.isTalking == false)
        {
            PlayerMovement.instance.enabled = true;
        }
        StartCoroutine(PauseClose());
        if(CurrentSceneManager.instance.temporange == true)
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1;
        }
        gameIsPaused = false;
    }

    public void MainMenuButton()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryButton()
    {
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedUpInThisSceneCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerHealth.instance.Respawn();
        CurrentSceneManager.instance.active = false;
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

    public IEnumerator PauseOpen()
    {
        bulles.SetBool("isOpen", true);
        pauseText.SetBool("isOpen", true);
        if(CurrentSceneManager.instance.temporange == true)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        } 
    }

    public IEnumerator PauseClose()
    {
        bulles.SetBool("isOpen", false);
        pauseText.SetBool("isOpen", false);
        if(CurrentSceneManager.instance.temporange == true)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        } 
        pauseMenuUI.SetActive(false);
    }
}
