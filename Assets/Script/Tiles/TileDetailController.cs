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
    private Image header;

    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI infoText;

    [SerializeField]
    private TextMeshProUGUI priceText;

    [SerializeField]
    private GameObject buyBtn;

    [SerializeField]
    private GameObject sellBtn;

    [SerializeField]
    private GameObject ownerPanel;

    [SerializeField]
    private TextMeshProUGUI ownerText;

    [SerializeField]
    private GameObject ownerIcon;

    [SerializeField]
    private TileColorSprite[] spriteColors;

    [SerializeField]
    private GameObject buyOptions;

    [SerializeField]
    private GameObject plusBtn;

    [SerializeField]
    private GameObject minusBtn;

    [SerializeField]
    private GameObject buyOptionsHouse;

    [SerializeField]
    private GameObject buyOptionsHotel;

    [SerializeField]
    private GameManager gameManager;

    private Tile curTile;
    private Player curPlayer;

    // Start is called before the first frame update
    void Start() {
        ClearPanel();

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void ShowDetails(Tile tile, Player player) {
        curTile = tile;
        curPlayer = player;

        if (curTile == null || curPlayer == null) {
            return;
        }

        nameText.text = tile.GetTitle();
        UpdateColor(tile.GetColor());
        UpdateInfoAndPriceText(tile);
        UpdateButtons(tile, player);

        panel.SetActive(true);
        panelExit.SetActive(true);
    }

    private void UpdateColor(TileColor color) {
        foreach (TileColorSprite cs in spriteColors) {
            if (color == cs.tileColor) {
                header.sprite = cs.tileSprite;
                break;
            }
        }
    }

    private void UpdateInfoAndPriceText(Tile tile) {
        TileDetails details = tile.GetDetails();
        TileStatus status = tile.GetStatus();

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

            if (i + 1 == (int)status) {
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

    private void UpdateButtons(Tile tile, Player player) {
        ClearButtons();

        TileDetails details = tile.GetDetails();
        TileStatus status = tile.GetStatus();

        if (status == TileStatus.NOT_BOUGHT) {
            if (gameManager.GetPlayerCurPosition(player.GetId()) == tile.GetId()) {
                buyBtn.SetActive(true);

                return;
            }

            ownerText.text = "Proprietário: <b>Não tem</b>";
            ownerPanel.SetActive(true);

            return;
        }

        if (status == TileStatus.MORTGAGE) {
            ownerText.text = "Proprietário: <b>Banco</b>";
            ownerPanel.SetActive(true);

            return;
        }

        Player owner = details.GetOwner();
        if (owner != null && owner.GetId() != player.GetId()) {
            ownerText.text = "Proprietário:";
            ownerIcon.GetComponent<Image>().sprite = owner.GetImage();
            ownerIcon.SetActive(true);
            ownerPanel.SetActive(true);

            return;
        }

        switch (status) {
            case TileStatus.PURCHASED:
                sellBtn.SetActive(true);
                plusBtn.SetActive(true);
                buyOptionsHouse.SetActive(true);
                break;
            case TileStatus.ONE_HOUSE: case TileStatus.TWO_HOUSES: case TileStatus.THREE_HOUSES: case TileStatus.FOUR_HOUSES:
                minusBtn.SetActive(true);
                plusBtn.SetActive(true);
                buyOptionsHouse.SetActive(true);
                break;
            case TileStatus.HOTEL:
                minusBtn.SetActive(true);
                buyOptionsHotel.SetActive(true);
                break;
        }
        buyOptions.SetActive(true);
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

    private void UpdateDetails() {
        UpdateInfoAndPriceText(curTile);
        UpdateButtons(curTile, curPlayer);
    }

    public void BuyProperty() {
        if (curTile == null || curPlayer == null) {
            return;
        }

        // TODO: Add validations and purchase confirmation

        curTile.BuyProperty(curPlayer);
        UpdateDetails();
    }

    public void SellProperty() {
        if (curTile == null || curPlayer == null) {
            return;
        }

        // TODO: Add validations and purchase confirmation

        curTile.SellProperty();
        UpdateDetails();
    }



    public void AddHouse() {
        if (curTile == null || curPlayer == null) {
            return;
        }

        // TODO: Add validations and purchase confirmation

        curTile.UpgradeProperty();
        UpdateDetails();
    }

    public void RemoveHouse() {
        if (curTile == null || curPlayer == null) {
            return;
        }

        // TODO: Add validations and sell confirmation

        curTile.DowngradeProperty();
        UpdateDetails();
    }

    public void ClearPanel() {
        curTile = null;
        curPlayer = null;
        nameText.text = "";
        infoText.text = "";
        priceText.text = "";
        ClearButtons();
        panel.SetActive(false);
        panelExit.SetActive(false);
    }

    private void ClearButtons() {
        buyBtn.SetActive(false);
        sellBtn.SetActive(false);
        ownerIcon.SetActive(false);
        ownerPanel.SetActive(false);
        buyOptions.SetActive(false);
        minusBtn.SetActive(false);
        plusBtn.SetActive(false);
        buyOptionsHouse.SetActive(false);
        buyOptionsHotel.SetActive(false);
    }
}
