using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public GameObject settingsWindow;

    public void Start()
    {
        


    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
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
