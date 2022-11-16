using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPanel : MonoBehaviour
{
    [SerializeField] private GameObject redPanel;
    [SerializeField] private GameObject bluePanel;
    [SerializeField] private GameObject greenPanel;
    [SerializeField] private GameObject yellowPanel;

    private void UpdateRedCurrency(int value) {    
        if (!redPanel.activeSelf) {
            redPanel.SetActive(true);
        }

        redPanel.GetComponent<Currency>().UpdateCurrency(value);
    }

    private void UpdateBlueCurrency(int value) {
        if (!bluePanel.activeSelf) {
            bluePanel.SetActive(true);
        }

        bluePanel.GetComponent<Currency>().UpdateCurrency(value);
    }

    private void UpdateGreenCurrency(int value) {
        if (!greenPanel.activeSelf) {
            greenPanel.SetActive(true);
        }

        greenPanel.GetComponent<Currency>().UpdateCurrency(value);
    }

    private void UpdateYellowCurrency(int value) {
        if (!yellowPanel.activeSelf) {
            yellowPanel.SetActive(true);
        }

        yellowPanel.GetComponent<Currency>().UpdateCurrency(value);
    }

    public void UpdateCurrency(Player player) {
        int value = player.Money;
        PlayerColor color = player.Color;

        switch (color) {
            case PlayerColor.BLUE:
                UpdateBlueCurrency(value);
                break;
            case PlayerColor.GREEN:
                UpdateGreenCurrency(value);
                break;
            case PlayerColor.RED:
                UpdateRedCurrency(value);
                break;
            case PlayerColor.YELLOW:
                UpdateYellowCurrency(value);
                break;
        }
    }
}
