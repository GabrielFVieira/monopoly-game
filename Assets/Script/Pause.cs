using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private string Menu = "Menu";
    private bool isPaused;
    public GameObject pausePanel;
    public GameObject pauseBtn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen();
        }
    }

    void PauseScreen()
    {
        if (isPaused)
        {
            isPaused = false;
            pausePanel.SetActive(false);
            pauseBtn.SetActive(true);
            Time.timeScale = 1;
        }
        else
        {
            isPaused = true;
            pausePanel.SetActive(true);
            pauseBtn.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        pauseBtn.SetActive(true);
        Time.timeScale = 1;
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene(Menu);
    }

    public void PauseButton()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        pauseBtn.SetActive(false);
        Time.timeScale = 0;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        if (Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    /*
    public AudioMixer mainMixer;
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }
    */
}

