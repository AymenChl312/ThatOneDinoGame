using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider musicSlider;
    public Slider soundSlider;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public bool isFullscreen;

    public GameObject inGameToggle;
    Resolution[] resolutions;

    public static SettingsMenu instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de SettingsMenu dans la scene.");
            return;
        }
        instance = this;
    }

    public void Start()
    {
        inGameToggle = GameObject.Find("FullScreen");
        
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            Load();
        }

        else
        {
            ChangeSoundVolume();
            ChangeMusicVolume();
        }

        if(PlayerPrefs.HasKey("Fullscreen"))
        {
            if(PlayerPrefs.GetInt("Fullscreen") == 1)
            {
                inGameToggle.GetComponent<Toggle>().isOn = true;
                isFullscreen = true;
            }
            else
            {
                inGameToggle.GetComponent<Toggle>().isOn = false;
                isFullscreen = false;
            }

        }
        else
        {
            SetFullScreen(isFullscreen);
        }


    }

    public void ChangeMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void ChangeSoundVolume()
    {
        float volume = soundSlider.value;
        audioMixer.SetFloat("Sound", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    public void Load()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");

        ChangeMusicVolume();
        ChangeSoundVolume();
    }



    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (isFullScreen)
        {
            inGameToggle.GetComponent<Toggle>().isOn = true;
            PlayerPrefs.SetInt("Fullscreen", 1);
            isFullscreen = true;

        }
        else
        {
            inGameToggle.GetComponent<Toggle>().isOn = false;
            PlayerPrefs.SetInt("Fullscreen", 0);
            isFullscreen = false;
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void clearSavedData()
    {
        PlayerPrefs.DeleteAll();
    }
}
