using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class TileColorSprite
{
    public TileColor tileColor;
    public Sprite tileSprite;
}

public class Tile : BaseTile {
    [SerializeField]
    private string title;

    [SerializeField]
    private TileColor color;

    [SerializeField]
    private int price;

    [SerializeField]
    private TileStatus status;

    [SerializeField]
    private TileDetails details = new();

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

    [SerializeField]
    private GameObject highlight;

    private int initialSortingOrder;

    // Start is called before the first frame update
    void Start() {
        FillControlObjects();

        initialSortingOrder = GetComponent<SortingGroup>().sortingOrder;

        status = TileStatus.NOT_BOUGHT;
        ownerIcon.SetActive(false);
        nameObj.text = title;
        priceObj.text = FormatPrice(price);
        highlight.SetActive(false);

        foreach (TileColorSprite cs in spriteColors) {
            if (color == cs.tileColor) {
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

        if (waypoint == null) {
            SetWaypoint();
        }

        if (tileDetailController == null) {
            tileDetailController = GameObject.FindGameObjectWithTag("TileDetail").GetComponent<TileDetailController>();
        }
    }

    // Update is called once per frame
    void Update() {
        SetTileStatus(status); // TODO: Remove after tests
    }

    public void SetTileStatus(TileStatus newStatus) {
        status = newStatus;

        if (status >= TileStatus.ONE_HOUSE && status <= TileStatus.FOUR_HOUSES) {
            int amount = (int)status - 1;
            houseCountTextObj.text = "x" + amount.ToString();
            houseCountObj.SetActive(true);
            hotelObj.SetActive(false);
        } else if (status == TileStatus.HOTEL) {
            hotelObj.SetActive(true);
            houseCountObj.SetActive(false);
        } else {
            hotelObj.SetActive(false);
            houseCountObj.SetActive(false);
        }
    }

    private void SetOwner(Player player) {
        if (player == null) {
            details.SetOwner(null);
            ownerIcon.SetActive(false);

            return;
        }

        details.SetOwner(player);
        ownerIcon.GetComponent<SpriteRenderer>().color = player.GetColor();
        ownerIcon.SetActive(true);
    }

    private void OnMouseUp() {
        Player curPlayer = gameManager.GetCurrentPlayer();

        if (curPlayer != null) {
            ShowDetails(curPlayer);            
        }
    }

    private void OnMouseOver()
    {
        
    }

    private void OnMouseEnter() {
        highlight.SetActive(true);
        GetComponent<SortingGroup>().sortingOrder = initialSortingOrder + 5;
    }

    private void OnMouseExit() {
        highlight.SetActive(false);
        GetComponent<SortingGroup>().sortingOrder = initialSortingOrder;
    }

    private string FormatPrice(int price) {      
        return "$" + price.ToString("N0");
    }

    private void ShowDetails(Player player) {
        tileDetailController.ShowDetails(this, player);
    }

    public override void ExecuteAction(Player player) {
        ShowDetails(player);
    }

    public TileDetails GetDetails() {
        return details;
    }

    public string GetTitle() {
        return title;
    }

    public int GetPrice() {
        return price;
    }

    public TileColor GetColor() {
        return color;
    }

    public TileStatus GetStatus() {
        return status;
    }

    public void BuyProperty(Player player) {
        if (status != TileStatus.NOT_BOUGHT) {
            // Throw error
            return;
        }

        status = TileStatus.PURCHASED;
        SetOwner(player);
    }

    public void SellProperty() {
        status = TileStatus.NOT_BOUGHT;
        SetOwner(null);
    }

    public void UpgradeProperty() {
        if (status >= TileStatus.PURCHASED && status < TileStatus.HOTEL) {
            status++;
        }                
    }

    public void DowngradeProperty() {
        if (status > TileStatus.PURCHASED) {
            status--;
        }
    }
}
