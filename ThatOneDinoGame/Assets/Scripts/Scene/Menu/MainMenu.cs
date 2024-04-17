using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject settingsWindow;

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (PlayerPrefs.GetInt("levelReached") > 0)
            {
                SceneManager.LoadScene("LevelSelect");
            }
            else
            {
                SceneManager.LoadScene("Level0");
            }
            
        }
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(false);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
