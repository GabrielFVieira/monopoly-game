using TMPro;
using UnityEngine;

public class Tile : BaseTile {

    [field: SerializeField]
    public string Title { get; private set; }

    [field: SerializeField]
    public TileColor Color { get; private set; }

    [field: SerializeField]
    public int Price { get; private set; }

    [field: SerializeField]
    public TileStatus Status { get; private set; }

    [field: SerializeField]
    public TileDetails Details { get; private set; } = new();

    [field: SerializeField]
    public Player Owner { get; private set; }

    [SerializeField]
    private TextMeshPro nameObj;

    [SerializeField]
    private TextMeshPro priceObj;

    [SerializeField]
    private GameObject houseCountObj;

    [SerializeField]
    private TextMeshPro houseCountTextObj;

    [SerializeField]
    private GameObject hotelObj;

    [SerializeField]
    private TileColorSprite[] spriteColors;

    [SerializeField]
    private GameObject ownerIcon;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private TileDetailController tileDetailController;

    // Start is called before the first frame update
    new void Start() {
        base.Start();
        FillControlObjects();

        Status = TileStatus.NOT_BOUGHT;
        ownerIcon.SetActive(false);
        nameObj.text = Title;
        priceObj.text = Utils.FormatPrice(Price);
        highlight.SetActive(false);

        foreach (TileColorSprite cs in spriteColors) {
            if (Color == cs.tileColor) {
                GetComponent<SpriteRenderer>().sprite = cs.tileSprite;
                break;
            }
        }
    }

    private void FillControlObjects() {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        if (nameObj == null) {
            nameObj = gameObject.transform.Find("Name").GetComponent<TextMeshPro>();
        }

        if (priceObj == null) {
            priceObj = gameObject.transform.Find("Price").GetComponent<TextMeshPro>();
        }

        if (ownerIcon == null) {
            ownerIcon = gameObject.transform.Find("Owner").gameObject;
        }

        if (tileDetailController == null) {
            tileDetailController = GameObject.FindGameObjectWithTag("TileDetail").GetComponent<TileDetailController>();
        }
    }

    // Update is called once per frame
    void Update() {
        SetTileStatus(Status); // TODO: Remove after tests
    }

    private void OnMouseUp() {
        Player curPlayer = gameManager.GetCurrentPlayer();

        if (curPlayer != null) {
            ShowDetails(curPlayer);            
        }
    }

    public override void ExecuteAction(Player player) {
        // TODO: Cobrar jogador caso seja pertencente a outro
    }

    private void ShowDetails(Player player) {
        tileDetailController.ShowDetails(this, player);
    }

    private void SetTileStatus(TileStatus newStatus) {
        Status = newStatus;

        if (Status >= TileStatus.ONE_HOUSE && Status <= TileStatus.FOUR_HOUSES) {
            int amount = (int)Status - 1;
            houseCountTextObj.text = "x" + amount.ToString();
            houseCountObj.SetActive(true);
            hotelObj.SetActive(false);
        } else if (Status == TileStatus.HOTEL) {
            hotelObj.SetActive(true);
            houseCountObj.SetActive(false);
        } else {
            hotelObj.SetActive(false);
            houseCountObj.SetActive(false);
        }
    }

    private void UpdateOwner(Player player) {
        if (player == null) {
            Owner = null;
            ownerIcon.SetActive(false);

            return;
        }

        Owner = player;
        ownerIcon.GetComponent<SpriteRenderer>().color = player.GetColor();
        ownerIcon.SetActive(true);
    }

    public void BuyProperty(Player player) {
        if (Status != TileStatus.NOT_BOUGHT) {
            // Throw error
            return;
        }

        Status = TileStatus.PURCHASED;
        UpdateOwner(player);
    }

    public void SellProperty() {
        Status = TileStatus.NOT_BOUGHT;
        UpdateOwner(null);
    }

    public void UpgradeProperty() {
        if (Status >= TileStatus.PURCHASED && Status < TileStatus.HOTEL) {
            Status++;
        }                
    }

    public void DowngradeProperty() {
        if (Status > TileStatus.PURCHASED) {
            Status--;
        }
    }
}
