using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class TileColorSprite
{
    public TileColor tileColor;
    public Sprite tileSprite;
}

public class Tile : BaseTile {
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

    // Start is called before the first frame update
    void Start() {
        FillControlObjects();

        details.SetTileStatus(TileStatus.NOT_BOUGHT);
        ownerIcon.SetActive(false);
        nameObj.text = details.GetTitle();
        priceObj.text = FormatPrice(details.GetPrice());

        foreach (TileColorSprite cs in spriteColors) {
            if (details.GetColor() == cs.tileColor) {
                GetComponent<SpriteRenderer>().sprite = cs.tileSprite;
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
        SetTileStatus(details.GetStatus());
    }

    public void SetTileStatus(TileStatus status) {
        details.SetTileStatus(status);

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

    public void SetOwner(Player player) {
        if (player == null) {
            details.SetOwner(null);
            ownerIcon.SetActive(false);
        }

        details.SetOwner(player);
        ownerIcon.GetComponent<SpriteRenderer>().color = player.GetColor();
        ownerIcon.SetActive(true);
    }

    private void OnMouseDown() {
        Player curPlayer = gameManager.GetCurrentPlayer();

        if (curPlayer != null) {
            ShowDetails(curPlayer);            
        }
    }

    private void OnMouseOver()
    {
        //Debug.Log("Tile mouse over");
    }

    private string FormatPrice(int price) {      
        return "$" + price.ToString("N0");
    }

    private void ShowDetails(Player player) {
        tileDetailController.ShowDetails(id, details, player);
    }

    public override void ExecuteAction(Player player) {
        ShowDetails(player);
    }
}
