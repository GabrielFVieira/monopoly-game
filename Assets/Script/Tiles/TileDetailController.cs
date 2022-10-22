using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class TileDetailController : MonoBehaviour {
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject panelExit;

    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI infoText;

    [SerializeField]
    private TextMeshProUGUI priceText;

    [SerializeField]
    private GameObject buyBtn;

    [SerializeField]
    private GameObject ownerPanel;

    [SerializeField]
    private TextMeshProUGUI ownerText;

    [SerializeField]
    private GameObject ownerIcon;

    [SerializeField]
    private TileColorSprite[] spriteColors;

    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        panel.SetActive(false);
        panelExit.SetActive(false);

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void ShowDetails(int tileId, TileDetails details, Player player) {
        nameText.text = details.GetTitle();
        UpdateColor(details.GetColor());
        UpdateInfoAndPriceText(details);
        UpdateButtons(tileId, details, player);

        panel.SetActive(true);
        panelExit.SetActive(true);
    }

    private void UpdateColor(TileColor color) {
        foreach (TileColorSprite cs in spriteColors) {
            if (color == cs.tileColor) {
                // TODO: Update ColorPanel sprite
                // GetComponent<SpriteRenderer>().sprite = cs.tileSprite;
            }
        }
    }

    private void UpdateInfoAndPriceText(TileDetails details) {
        Dictionary<string, int> infos = new Dictionary<string, int>();
        infos.Add("Aluguel", details.GetRent());
        infos.Add("1 casa", details.GetHouse1());
        infos.Add("2 casas", details.GetHouse2());
        infos.Add("3 casas", details.GetHouse3());
        infos.Add("4 casas", details.GetHouse4());
        infos.Add("Hotel", details.GetHotel());

        string fullInfoText = "";
        string fullPriceText = "";

        int infosLength = infos.Count;

        for (int i = 0; i < infosLength; i++) {
            var item = infos.ElementAt(i);
            string infoText = item.Key;
            string priceText = FormatPrice(item.Value);

            if (i + 1 == (int)details.GetStatus()) {
                fullInfoText += HighlightText(infoText);
                fullPriceText += HighlightText(priceText);
            } else {
                fullInfoText += infoText;
                fullPriceText += priceText;
            }

            fullInfoText += AddSpace(i, infosLength);
            fullPriceText += AddSpace(i, infosLength);
        }

        infoText.text = fullInfoText;
        priceText.text = fullPriceText;
    }

    private void UpdateButtons(int tileId, TileDetails details, Player player) {
        if (details.GetStatus() == TileStatus.NOT_BOUGHT) {
            if (gameManager.GetPlayerCurPosition(player.GetId()) == tileId) {
                buyBtn.SetActive(true);
                ownerPanel.SetActive(false);

                return;
            }

            ownerText.text = "Proprietário: <b>Não tem</b>";
            buyBtn.SetActive(false);
            ownerIcon.SetActive(false);
            ownerPanel.SetActive(true);

            return;
        }

        // TODO: Add logic for when the tile was already purchased by the current player or another one
    }

    private string HighlightText(string text) {
        return "<color=#08FF00><b>" + text + "</b></color>";
    }

    private string FormatPrice(int price) {
        return "$" + price.ToString("N0");
    }

    private string AddSpace(int index, int limit) {
        if (index == 0) {
            return "\n\n";
        } else if (index < limit - 1) {
            return "\n";
        }

        return "";
    }
}
