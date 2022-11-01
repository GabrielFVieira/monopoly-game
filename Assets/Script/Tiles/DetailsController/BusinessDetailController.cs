using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class BusinessDetailController : MonoBehaviour {
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject panelExit;

    [SerializeField]
    private TextMeshProUGUI nameText;

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
    private GameManager gameManager;

    private BusinessTile curTile;
    private Player curPlayer;

    // Start is called before the first frame update
    void Start() {
        ClearPanel();

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void ShowDetails(BusinessTile tile, Player player) {
        curTile = tile;
        curPlayer = player;

        if (curTile == null || curPlayer == null) {
            return;
        }

        nameText.text = tile.Title;
        UpdatePriceText(tile);
        UpdateButtons(tile, player);

        panel.SetActive(true);
        panelExit.SetActive(true);
    }

    private void UpdatePriceText(BusinessTile tile) {
        string price = Utils.FormatPrice(tile.ValueMultiplier);
        if (tile.Status == TileStatus.PURCHASED) {
            price = Utils.HighlightText(price);
        }

        priceText.text = price;
    }

    private void UpdateButtons(BusinessTile tile, Player player) {
        ClearButtons();
        TileStatus status = tile.Status;

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

        Player owner = tile.Owner;
        if (owner != null && owner.GetId() != player.GetId()) {
            ownerText.text = "Proprietário:";
            ownerIcon.GetComponent<Image>().sprite = owner.GetImage();
            ownerIcon.SetActive(true);
            ownerPanel.SetActive(true);

            return;
        } else {
            sellBtn.SetActive(true);
        }
    }

    private void UpdateDetails() {
        UpdatePriceText(curTile);
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

    public void ClearPanel() {
        curTile = null;
        curPlayer = null;
        nameText.text = "";
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
    }
}
