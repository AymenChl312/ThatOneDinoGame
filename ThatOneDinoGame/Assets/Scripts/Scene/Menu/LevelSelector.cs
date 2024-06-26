using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 0);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i+1> levelReached)
            {
                levelButtons[i].interactable = false;
            }        
        }
    }
    public void LoadLevelPassed(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
