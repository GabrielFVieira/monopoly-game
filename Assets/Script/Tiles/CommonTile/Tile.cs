using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        Player curPlayer = gameManager.GetCurrentPlayer();
        if (curPlayer != null && !curPlayer.AI) {
            ShowDetails(curPlayer);            
        }
    }

    public override void ExecuteAction(Player player) {
        if (Owner != null && Owner.Id != player.Id) {
            int value;
            switch (Status) {
                case TileStatus.ONE_HOUSE:
                    value = Details.House1;
                    break;
                case TileStatus.TWO_HOUSES:
                    value = Details.House2;
                    break;
                case TileStatus.THREE_HOUSES:
                    value = Details.House3;
                    break;
                case TileStatus.FOUR_HOUSES:
                    value = Details.House4;
                    break;
                case TileStatus.HOTEL:
                    value = Details.Hotel;
                    break;
                default:
                    value = Details.Rent;
                    break;
            }

            Debug.Log("Player " + player.Name + " paying $" + Utils.FormatPrice(value) + " to player " + Owner.Name);

            player.Pay(value);
            Owner.Receive(value);
        }
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
        ownerIcon.GetComponent<SpriteRenderer>().sprite = player.Icon;
        ownerIcon.SetActive(true);
    }

    public void BuyProperty(Player player) {
        if (Status != TileStatus.NOT_BOUGHT) {
            // Throw error
            return;
        }

        Status = TileStatus.PURCHASED;
        player.Pay(Price);
        UpdateOwner(player);
    }

    public void SellProperty() {
        Status = TileStatus.NOT_BOUGHT;
        Owner.Receive(Price / 2);
        UpdateOwner(null);
    }

    public void UpgradeProperty() {
        if (Status >= TileStatus.PURCHASED && Status < TileStatus.HOTEL) {
            Status++;

            if (TileStatus.HOTEL == Status) {
                Owner.Pay(Details.HotelPurchaseValue);
            } else {
                Owner.Pay(Details.HousePurchaseValue);
            }
        }                
    }

    public void DowngradeProperty() {
        if (Status > TileStatus.PURCHASED) {
            if (TileStatus.HOTEL == Status) {
                Owner.Receive(Details.HotelPurchaseValue / 2);
            } else {
                Owner.Receive(Details.HousePurchaseValue / 2);
            }

            Status--;
        }
    }
}
