using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    public static List<PlayerChoice> players = new();

    public static int readyPlayersAmount = 0;

    public AudioMixer mainMixer;

    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject selectPlayers;
    [SerializeField] private GameObject setPlayers;
    [SerializeField] private GameObject player3Setup;
    [SerializeField] private GameObject player4Setup;
    [SerializeField] private string rulesURL = "https://blog.estrela.com.br/como-jogar-banco-imobiliario/";


    public void Play() {
        painelMenuInicial.SetActive(false);
        selectPlayers.SetActive(true);
    }

    public void SelectPlayerAmount(int amount) {
        players = new();
        for (int i = 0; i < amount; i++) {
            PlayerChoice player = new();
            players.Add(player);
        }

        if (amount >= 3) {
            player3Setup.SetActive(true);
        }
        if (amount >= 4) {
            player4Setup.SetActive(true);
        }

        selectPlayers.SetActive(false);
        setPlayers.SetActive(true);
    }

    public static void LoadGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void OpenOptions() {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void CloseOptions() {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;

        if (Screen.fullScreen) {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        } else {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void OpenPage() {
        Application.OpenURL(rulesURL);
    }

    
    public void SetVolume(float volume) {
        mainMixer.SetFloat("volume", volume);
    }
}