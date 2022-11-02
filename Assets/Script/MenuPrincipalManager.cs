using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class MenuPrincipalManager : MonoBehaviour
{
    public static int playerAmount;
    public static List<Player> players = new List<Player>();
    [SerializeField] private string Jogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject selectPlayers;
    [SerializeField] private GameObject setPlayers;

    public void Jogar()
    {
        painelMenuInicial.SetActive(false);
        selectPlayers.SetActive(true);
        // SceneManager.LoadScene(Jogo);
    }
    public void SelectPlayerAmount(int amount) {
        playerAmount = amount;
        players = new List<Player>();
        for (int i = 0; i < playerAmount; i++) {
            Player player = new Player();
            players.Add(player);
        }
        selectPlayers.SetActive(false);
        setPlayers.SetActive(true);
        //SceneManager.LoadScene(Jogo);
    }
    public void SetPlayerPiece(string input) {
        string[] args = input.Split(',');
        int playerIndex = Convert.ToInt32(args[0] != null ? args[0] : "0");
        string piece = args[1];
        players[playerIndex].Piece = piece;
    }
    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }
    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }
    public void SairJogo()
    {
        Debug.Log(" Sair do Jogo ");
        Application.Quit();
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
    public void OpenPage()
    {
        Application.OpenURL("https://blog.estrela.com.br/como-jogar-banco-imobiliario/");
    }
    public AudioMixer mainMixer;
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }
}